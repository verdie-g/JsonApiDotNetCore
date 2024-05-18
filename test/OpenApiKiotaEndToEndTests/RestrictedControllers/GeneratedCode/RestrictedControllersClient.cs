// <auto-generated/>
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Store;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Serialization.Form;
using Microsoft.Kiota.Serialization.Json;
using Microsoft.Kiota.Serialization.Multipart;
using Microsoft.Kiota.Serialization.Text;
using OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode.DataStreams;
using OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode.ReadOnlyChannels;
using OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode.ReadOnlyResourceChannels;
using OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode.RelationshipChannels;
using OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode.WriteOnlyChannels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode {
    /// <summary>
    /// The main entry point of the SDK, exposes the configuration and the fluent API.
    /// </summary>
    public class RestrictedControllersClient : BaseRequestBuilder 
    {
        /// <summary>The dataStreams property</summary>
        public DataStreamsRequestBuilder DataStreams
        {
            get => new DataStreamsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The readOnlyChannels property</summary>
        public ReadOnlyChannelsRequestBuilder ReadOnlyChannels
        {
            get => new ReadOnlyChannelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The readOnlyResourceChannels property</summary>
        public ReadOnlyResourceChannelsRequestBuilder ReadOnlyResourceChannels
        {
            get => new ReadOnlyResourceChannelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The relationshipChannels property</summary>
        public RelationshipChannelsRequestBuilder RelationshipChannels
        {
            get => new RelationshipChannelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>The writeOnlyChannels property</summary>
        public WriteOnlyChannelsRequestBuilder WriteOnlyChannels
        {
            get => new WriteOnlyChannelsRequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new <see cref="RestrictedControllersClient"/> and sets the default values.
        /// </summary>
        /// <param name="backingStore">The backing store to use for the models.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public RestrictedControllersClient(IRequestAdapter requestAdapter, IBackingStoreFactory backingStore = default) : base(requestAdapter, "{+baseurl}", new Dictionary<string, object>())
        {
            ApiClientBuilder.RegisterDefaultSerializer<JsonSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<TextSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<FormSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<MultipartSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<JsonParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<TextParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<FormParseNodeFactory>();
            if (string.IsNullOrEmpty(RequestAdapter.BaseUrl))
            {
                RequestAdapter.BaseUrl = "http://localhost";
            }
            PathParameters.TryAdd("baseurl", RequestAdapter.BaseUrl);
            RequestAdapter.EnableBackingStore(backingStore);
        }
    }
}
