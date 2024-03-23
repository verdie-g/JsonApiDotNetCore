using JetBrains.Annotations;
using JsonApiDotNetCore.OpenApi.Client.NSwag;
using Newtonsoft.Json;

namespace OpenApiNSwagClientExample;

[UsedImplicitly(ImplicitUseTargetFlags.Itself)]
public partial class ExampleApiClient : JsonApiClient
{
    partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
    {
        SetSerializerSettingsForJsonApi(settings);

#if DEBUG
        settings.Formatting = Formatting.Indented;
#endif
    }
}