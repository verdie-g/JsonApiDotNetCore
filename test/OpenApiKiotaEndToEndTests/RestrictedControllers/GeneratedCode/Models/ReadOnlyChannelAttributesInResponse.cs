// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenApiKiotaEndToEndTests.RestrictedControllers.GeneratedCode.Models {
    #pragma warning disable CS1591
    public class ReadOnlyChannelAttributesInResponse : IBackedModel, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>Stores model information.</summary>
        public IBackingStore BackingStore { get; private set; }
        /// <summary>The isAdultOnly property</summary>
        public bool? IsAdultOnly {
            get { return BackingStore?.Get<bool?>("isAdultOnly"); }
            set { BackingStore?.Set("isAdultOnly", value); }
        }
        /// <summary>The isCommercial property</summary>
        public bool? IsCommercial {
            get { return BackingStore?.Get<bool?>("isCommercial"); }
            set { BackingStore?.Set("isCommercial", value); }
        }
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name {
            get { return BackingStore?.Get<string?>("name"); }
            set { BackingStore?.Set("name", value); }
        }
#nullable restore
#else
        public string Name {
            get { return BackingStore?.Get<string>("name"); }
            set { BackingStore?.Set("name", value); }
        }
#endif
        /// <summary>
        /// Instantiates a new <see cref="ReadOnlyChannelAttributesInResponse"/> and sets the default values.
        /// </summary>
        public ReadOnlyChannelAttributesInResponse()
        {
            BackingStore = BackingStoreFactorySingleton.Instance.CreateBackingStore();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ReadOnlyChannelAttributesInResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ReadOnlyChannelAttributesInResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ReadOnlyChannelAttributesInResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"isAdultOnly", n => { IsAdultOnly = n.GetBoolValue(); } },
                {"isCommercial", n => { IsCommercial = n.GetBoolValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteBoolValue("isAdultOnly", IsAdultOnly);
            writer.WriteBoolValue("isCommercial", IsCommercial);
            writer.WriteStringValue("name", Name);
        }
    }
}
