using System.Reflection;
using JsonApiDotNetCore.Errors;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.OpenApi.JsonApiMetadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace JsonApiDotNetCore.OpenApi;

/// <summary>
/// Adds JsonApiDotNetCore metadata to <see cref="ControllerActionDescriptor" />s if available. This translates to updating response types in
/// <see cref="ProducesResponseTypeAttribute" /> and performing an expansion for secondary and relationship endpoints (eg
/// /article/{id}/{relationshipName} -> /article/{id}/author, /article/{id}/revisions, etc).
/// </summary>
internal sealed class JsonApiActionDescriptorCollectionProvider : IActionDescriptorCollectionProvider
{
    private readonly IActionDescriptorCollectionProvider _defaultProvider;
    private readonly JsonApiEndpointMetadataProvider _jsonApiEndpointMetadataProvider;

    public ActionDescriptorCollection ActionDescriptors => GetActionDescriptors();

    public JsonApiActionDescriptorCollectionProvider(IActionDescriptorCollectionProvider defaultProvider,
        JsonApiEndpointMetadataProvider jsonApiEndpointMetadataProvider)
    {
        ArgumentGuard.NotNull(defaultProvider);
        ArgumentGuard.NotNull(jsonApiEndpointMetadataProvider);

        _defaultProvider = defaultProvider;
        _jsonApiEndpointMetadataProvider = jsonApiEndpointMetadataProvider;
    }

    private ActionDescriptorCollection GetActionDescriptors()
    {
        List<ActionDescriptor> newDescriptors = _defaultProvider.ActionDescriptors.Items.ToList();
        List<ActionDescriptor> endpoints = newDescriptors.Where(IsVisibleJsonApiEndpoint).ToList();

        foreach (ActionDescriptor endpoint in endpoints)
        {
            MethodInfo actionMethod = endpoint.GetActionMethod();
            JsonApiEndpointMetadataContainer endpointMetadataContainer = _jsonApiEndpointMetadataProvider.Get(actionMethod);

            List<ActionDescriptor> replacementDescriptorsForEndpoint =
            [
                .. AddJsonApiMetadataToAction(endpoint, endpointMetadataContainer.RequestMetadata),
                .. AddJsonApiMetadataToAction(endpoint, endpointMetadataContainer.ResponseMetadata)
            ];

            if (replacementDescriptorsForEndpoint.Any())
            {
                newDescriptors.InsertRange(newDescriptors.IndexOf(endpoint) - 1, replacementDescriptorsForEndpoint);
                newDescriptors.Remove(endpoint);
            }
        }

        int descriptorVersion = _defaultProvider.ActionDescriptors.Version;
        return new ActionDescriptorCollection(newDescriptors.AsReadOnly(), descriptorVersion);
    }

    private static bool IsVisibleJsonApiEndpoint(ActionDescriptor descriptor)
    {
        // Only if in a convention ApiExplorer.IsVisible was set to false, the ApiDescriptionActionData will not be present.
        return descriptor is ControllerActionDescriptor controllerAction && controllerAction.Properties.ContainsKey(typeof(ApiDescriptionActionData));
    }

    private static IEnumerable<ActionDescriptor> AddJsonApiMetadataToAction(ActionDescriptor endpoint, IJsonApiEndpointMetadata? jsonApiEndpointMetadata)
    {
        switch (jsonApiEndpointMetadata)
        {
            case PrimaryResponseMetadata primaryMetadata:
            {
                UpdateProducesResponseTypeAttribute(endpoint, primaryMetadata.DocumentType);
                return Array.Empty<ActionDescriptor>();
            }
            case PrimaryRequestMetadata primaryMetadata:
            {
                UpdateBodyParameterDescriptor(endpoint, primaryMetadata.DocumentType, null);
                return Array.Empty<ActionDescriptor>();
            }
            case NonPrimaryEndpointMetadata nonPrimaryEndpointMetadata and (RelationshipResponseMetadata or SecondaryResponseMetadata):
            {
                return Expand(endpoint, nonPrimaryEndpointMetadata,
                    (expandedEndpoint, documentType, _) => UpdateProducesResponseTypeAttribute(expandedEndpoint, documentType));
            }
            case NonPrimaryEndpointMetadata nonPrimaryEndpointMetadata and RelationshipRequestMetadata:
            {
                return Expand(endpoint, nonPrimaryEndpointMetadata, UpdateBodyParameterDescriptor);
            }
            default:
            {
                return Array.Empty<ActionDescriptor>();
            }
        }
    }

    private static void UpdateProducesResponseTypeAttribute(ActionDescriptor endpoint, Type responseDocumentType)
    {
        if (ProducesJsonApiResponseDocument(endpoint))
        {
            var producesResponse = endpoint.GetFilterMetadata<ProducesResponseTypeAttribute>();

            if (producesResponse != null)
            {
                producesResponse.Type = responseDocumentType;
                return;
            }
        }

        throw new UnreachableCodeException();
    }

    private static bool ProducesJsonApiResponseDocument(ActionDescriptor endpoint)
    {
        var produces = endpoint.GetFilterMetadata<ProducesAttribute>();

        return produces != null && produces.ContentTypes.Any(contentType => contentType == HeaderConstants.MediaType);
    }

    private static IEnumerable<ActionDescriptor> Expand(ActionDescriptor genericEndpoint, NonPrimaryEndpointMetadata metadata,
        Action<ActionDescriptor, Type, string> expansionCallback)
    {
        var expansion = new List<ActionDescriptor>();

        foreach ((string relationshipName, Type documentType) in metadata.DocumentTypesByRelationshipName)
        {
            if (genericEndpoint.AttributeRouteInfo == null)
            {
                throw new NotSupportedException("Only attribute routing is supported for JsonApiDotNetCore endpoints.");
            }

            ActionDescriptor expandedEndpoint = Clone(genericEndpoint);

            RemovePathParameter(expandedEndpoint.Parameters, JsonApiPathParameter.RelationshipName);

            ExpandTemplate(expandedEndpoint.AttributeRouteInfo!, relationshipName);

            expansionCallback(expandedEndpoint, documentType, relationshipName);

            expansion.Add(expandedEndpoint);
        }

        return expansion;
    }

    private static void UpdateBodyParameterDescriptor(ActionDescriptor endpoint, Type documentType, string? parameterName)
    {
        ControllerParameterDescriptor? requestBodyDescriptor = endpoint.GetBodyParameterDescriptor();

        if (requestBodyDescriptor == null)
        {
            MethodInfo actionMethod = endpoint.GetActionMethod();

            throw new InvalidConfigurationException(
                $"The action method '{actionMethod}' on type '{actionMethod.ReflectedType?.FullName}' contains no parameter with a [FromBody] attribute.");
        }

        requestBodyDescriptor.ParameterType = documentType;
        requestBodyDescriptor.ParameterInfo = new ParameterInfoWrapper(requestBodyDescriptor.ParameterInfo, documentType, parameterName);
    }

    private static ActionDescriptor Clone(ActionDescriptor descriptor)
    {
        var clone = (ActionDescriptor)descriptor.MemberwiseClone();

        clone.AttributeRouteInfo = (AttributeRouteInfo)descriptor.AttributeRouteInfo!.MemberwiseClone();

        clone.FilterDescriptors = new List<FilterDescriptor>();

        foreach (FilterDescriptor filter in descriptor.FilterDescriptors)
        {
            clone.FilterDescriptors.Add(Clone(filter));
        }

        clone.Parameters = new List<ParameterDescriptor>();

        foreach (ParameterDescriptor parameter in descriptor.Parameters)
        {
            clone.Parameters.Add((ParameterDescriptor)parameter.MemberwiseClone());
        }

        return clone;
    }

    private static FilterDescriptor Clone(FilterDescriptor descriptor)
    {
        var clone = (IFilterMetadata)descriptor.Filter.MemberwiseClone();

        return new FilterDescriptor(clone, descriptor.Scope)
        {
            Order = descriptor.Order
        };
    }

    private static void RemovePathParameter(ICollection<ParameterDescriptor> parameters, string parameterName)
    {
        ParameterDescriptor relationshipName = parameters.Single(parameterDescriptor => parameterDescriptor.Name == parameterName);

        parameters.Remove(relationshipName);
    }

    private static void ExpandTemplate(AttributeRouteInfo route, string expansionParameter)
    {
        route.Template = route.Template!.Replace(JsonApiRoutingTemplate.RelationshipNameUrlPlaceholder, expansionParameter);
    }
}