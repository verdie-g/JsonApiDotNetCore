using JetBrains.Annotations;
using JsonApiDotNetCore.OpenApi.JsonApiObjects.ResourceObjects;
using JsonApiDotNetCore.Resources;

namespace JsonApiDotNetCore.OpenApi.JsonApiObjects.Relationships;

[UsedImplicitly(ImplicitUseTargetFlags.Members)]
internal sealed class NullableToOneRelationshipInRequest<TResource> : NullableSingleData<ResourceIdentifier<TResource>>
    where TResource : IIdentifiable;