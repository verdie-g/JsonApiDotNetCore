// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenApiKiotaEndToEndTests.Links.GeneratedCode.Models {
    #pragma warning disable CS1591
    public class ErrorTopLevelLinks : IBackedModel, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>Stores model information.</summary>
        public IBackingStore BackingStore { get; private set; }
        /// <summary>The describedby property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Describedby {
            get { return BackingStore?.Get<string?>("describedby"); }
            set { BackingStore?.Set("describedby", value); }
        }
#nullable restore
#else
        public string Describedby {
            get { return BackingStore?.Get<string>("describedby"); }
            set { BackingStore?.Set("describedby", value); }
        }
#endif
        /// <summary>The self property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Self {
            get { return BackingStore?.Get<string?>("self"); }
            set { BackingStore?.Set("self", value); }
        }
#nullable restore
#else
        public string Self {
            get { return BackingStore?.Get<string>("self"); }
            set { BackingStore?.Set("self", value); }
        }
#endif
        /// <summary>
        /// Instantiates a new <see cref="ErrorTopLevelLinks"/> and sets the default values.
        /// </summary>
        public ErrorTopLevelLinks()
        {
            BackingStore = BackingStoreFactorySingleton.Instance.CreateBackingStore();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="ErrorTopLevelLinks"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ErrorTopLevelLinks CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ErrorTopLevelLinks();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"describedby", n => { Describedby = n.GetStringValue(); } },
                {"self", n => { Self = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("describedby", Describedby);
            writer.WriteStringValue("self", Self);
        }
    }
}
