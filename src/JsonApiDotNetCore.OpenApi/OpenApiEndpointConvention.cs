using System.Net;
using System.Reflection;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.OpenApi.JsonApiMetadata;
using JsonApiDotNetCore.OpenApi.JsonApiObjects.Documents;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace JsonApiDotNetCore.OpenApi;

/// <summary>
/// Sets metadata on controllers for OpenAPI documentation generation by Swagger. Only targets JsonApiDotNetCore controllers.
/// </summary>
internal sealed class OpenApiEndpointConvention : IActionModelConvention
{
    private readonly IControllerResourceMapping _controllerResourceMapping;
    private readonly EndpointResolver _endpointResolver;
    private readonly IJsonApiOptions _options;

    public OpenApiEndpointConvention(IControllerResourceMapping controllerResourceMapping, EndpointResolver endpointResolver, IJsonApiOptions options)
    {
        ArgumentGuard.NotNull(controllerResourceMapping);
        ArgumentGuard.NotNull(endpointResolver);
        ArgumentGuard.NotNull(options);

        _controllerResourceMapping = controllerResourceMapping;
        _endpointResolver = endpointResolver;
        _options = options;
    }

    public void Apply(ActionModel action)
    {
        ArgumentGuard.NotNull(action);

        JsonApiEndpoint? endpoint = _endpointResolver.Get(action.ActionMethod);

        if (endpoint == null)
        {
            // Not a JSON:API controller, or a non-standard action method in a JSON:API controller, or an atomic:operations
            // controller. None of these are yet implemented, so hide them to avoid downstream crashes.
            action.ApiExplorer.IsVisible = false;
            return;
        }

        ResourceType? resourceType = _controllerResourceMapping.GetResourceTypeForController(action.Controller.ControllerType);

        if (resourceType == null)
        {
            throw new UnreachableCodeException();
        }

        if (ShouldSuppressEndpoint(endpoint.Value, resourceType))
        {
            action.ApiExplorer.IsVisible = false;
            return;
        }

        SetResponseMetadata(action, endpoint.Value, resourceType);
        SetRequestMetadata(action, endpoint.Value);
    }

    private bool ShouldSuppressEndpoint(JsonApiEndpoint endpoint, ResourceType resourceType)
    {
        if (!IsEndpointAvailable(endpoint, resourceType))
        {
            return true;
        }

        if (IsSecondaryOrRelationshipEndpoint(endpoint))
        {
            if (!resourceType.Relationships.Any())
            {
                return true;
            }

            if (endpoint is JsonApiEndpoint.DeleteRelationship or JsonApiEndpoint.PostRelationship)
            {
                return !resourceType.Relationships.OfType<HasManyAttribute>().Any();
            }
        }

        return false;
    }

    private static bool IsEndpointAvailable(JsonApiEndpoint endpoint, ResourceType resourceType)
    {
        JsonApiEndpoints availableEndpoints = GetGeneratedControllerEndpoints(resourceType);

        if (availableEndpoints == JsonApiEndpoints.None)
        {
            // Auto-generated controllers are disabled, so we can't know what to hide.
            // It is assumed that a handwritten JSON:API controller only provides action methods for what it supports.
            // To accomplish that, derive from BaseJsonApiController instead of JsonApiController.
            return true;
        }

        // For an overridden JSON:API action method in a partial class to show up, it's flag must be turned on in [Resource].
        // Otherwise, it is considered to be an action method that throws because the endpoint is unavailable.
        return endpoint switch
        {
            JsonApiEndpoint.GetCollection => availableEndpoints.HasFlag(JsonApiEndpoints.GetCollection),
            JsonApiEndpoint.GetSingle => availableEndpoints.HasFlag(JsonApiEndpoints.GetSingle),
            JsonApiEndpoint.GetSecondary => availableEndpoints.HasFlag(JsonApiEndpoints.GetSecondary),
            JsonApiEndpoint.GetRelationship => availableEndpoints.HasFlag(JsonApiEndpoints.GetRelationship),
            JsonApiEndpoint.Post => availableEndpoints.HasFlag(JsonApiEndpoints.Post),
            JsonApiEndpoint.PostRelationship => availableEndpoints.HasFlag(JsonApiEndpoints.PostRelationship),
            JsonApiEndpoint.Patch => availableEndpoints.HasFlag(JsonApiEndpoints.Patch),
            JsonApiEndpoint.PatchRelationship => availableEndpoints.HasFlag(JsonApiEndpoints.PatchRelationship),
            JsonApiEndpoint.Delete => availableEndpoints.HasFlag(JsonApiEndpoints.Delete),
            JsonApiEndpoint.DeleteRelationship => availableEndpoints.HasFlag(JsonApiEndpoints.DeleteRelationship),
            _ => throw new UnreachableCodeException()
        };
    }

    private static JsonApiEndpoints GetGeneratedControllerEndpoints(ResourceType resourceType)
    {
        var resourceAttribute = resourceType.ClrType.GetCustomAttribute<ResourceAttribute>();
        return resourceAttribute?.GenerateControllerEndpoints ?? JsonApiEndpoints.None;
    }

    private static bool IsSecondaryOrRelationshipEndpoint(JsonApiEndpoint endpoint)
    {
        return endpoint is JsonApiEndpoint.GetSecondary or JsonApiEndpoint.GetRelationship or JsonApiEndpoint.PostRelationship or
            JsonApiEndpoint.PatchRelationship or JsonApiEndpoint.DeleteRelationship;
    }

    private void SetResponseMetadata(ActionModel action, JsonApiEndpoint endpoint, ResourceType resourceType)
    {
        action.Filters.Add(new ProducesAttribute(HeaderConstants.MediaType));

        foreach (HttpStatusCode statusCode in GetSuccessStatusCodesForEndpoint(endpoint))
        {
            // The return type is set later by JsonApiActionDescriptorCollectionProvider.
            action.Filters.Add(new ProducesResponseTypeAttribute((int)statusCode));
        }

        foreach (HttpStatusCode statusCode in GetErrorStatusCodesForEndpoint(endpoint, resourceType))
        {
            action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponseDocument), (int)statusCode));
        }
    }

    private static IEnumerable<HttpStatusCode> GetSuccessStatusCodesForEndpoint(JsonApiEndpoint endpoint)
    {
        return endpoint switch
        {
            JsonApiEndpoint.GetCollection or JsonApiEndpoint.GetSingle or JsonApiEndpoint.GetSecondary or JsonApiEndpoint.GetRelationship
                => [HttpStatusCode.OK],
            JsonApiEndpoint.Post =>
            [
                HttpStatusCode.Created,
                HttpStatusCode.NoContent
            ],
            JsonApiEndpoint.Patch =>
            [
                HttpStatusCode.OK,
                HttpStatusCode.NoContent
            ],
            JsonApiEndpoint.Delete or JsonApiEndpoint.PostRelationship or JsonApiEndpoint.PatchRelationship or JsonApiEndpoint.DeleteRelationship =>
            [
                HttpStatusCode.NoContent
            ],
            _ => throw new UnreachableCodeException()
        };
    }

    private IEnumerable<HttpStatusCode> GetErrorStatusCodesForEndpoint(JsonApiEndpoint endpoint, ResourceType resourceType)
    {
        ClientIdGenerationMode clientIdGeneration = resourceType.ClientIdGeneration ?? _options.ClientIdGeneration;

        return endpoint switch
        {
            JsonApiEndpoint.GetCollection => [HttpStatusCode.BadRequest],
            JsonApiEndpoint.GetSingle or JsonApiEndpoint.GetSecondary or JsonApiEndpoint.GetRelationship =>
            [
                HttpStatusCode.BadRequest,
                HttpStatusCode.NotFound
            ],
            JsonApiEndpoint.Post when clientIdGeneration == ClientIdGenerationMode.Forbidden =>
            [
                HttpStatusCode.BadRequest,
                HttpStatusCode.Forbidden,
                HttpStatusCode.Conflict,
                HttpStatusCode.UnprocessableEntity
            ],
            JsonApiEndpoint.Post =>
            [
                HttpStatusCode.BadRequest,
                HttpStatusCode.Conflict,
                HttpStatusCode.UnprocessableEntity
            ],
            JsonApiEndpoint.Patch =>
            [
                HttpStatusCode.BadRequest,
                HttpStatusCode.NotFound,
                HttpStatusCode.Conflict,
                HttpStatusCode.UnprocessableEntity
            ],
            JsonApiEndpoint.Delete => [HttpStatusCode.NotFound],
            JsonApiEndpoint.PostRelationship or JsonApiEndpoint.PatchRelationship or JsonApiEndpoint.DeleteRelationship => new[]
            {
                HttpStatusCode.BadRequest,
                HttpStatusCode.NotFound,
                HttpStatusCode.Conflict
            },
            _ => throw new UnreachableCodeException()
        };
    }

    private static void SetRequestMetadata(ActionModel action, JsonApiEndpoint endpoint)
    {
        if (RequiresRequestBody(endpoint))
        {
            action.Filters.Add(new ConsumesAttribute(HeaderConstants.MediaType));
        }
    }

    private static bool RequiresRequestBody(JsonApiEndpoint endpoint)
    {
        return endpoint is JsonApiEndpoint.Post or JsonApiEndpoint.Patch or JsonApiEndpoint.PostRelationship or JsonApiEndpoint.PatchRelationship or
            JsonApiEndpoint.DeleteRelationship;
    }
}
