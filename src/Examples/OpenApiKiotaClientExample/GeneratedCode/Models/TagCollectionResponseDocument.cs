// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenApiKiotaClientExample.GeneratedCode.Models {
    #pragma warning disable CS1591
    public class TagCollectionResponseDocument : IBackedModel, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>Stores model information.</summary>
        public IBackingStore BackingStore { get; private set; }
        /// <summary>The data property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<TagDataInResponse>? Data {
            get { return BackingStore?.Get<List<TagDataInResponse>?>("data"); }
            set { BackingStore?.Set("data", value); }
        }
#nullable restore
#else
        public List<TagDataInResponse> Data {
            get { return BackingStore?.Get<List<TagDataInResponse>>("data"); }
            set { BackingStore?.Set("data", value); }
        }
#endif
        /// <summary>The included property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<DataInResponse>? Included {
            get { return BackingStore?.Get<List<DataInResponse>?>("included"); }
            set { BackingStore?.Set("included", value); }
        }
#nullable restore
#else
        public List<DataInResponse> Included {
            get { return BackingStore?.Get<List<DataInResponse>>("included"); }
            set { BackingStore?.Set("included", value); }
        }
#endif
        /// <summary>The links property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ResourceCollectionTopLevelLinks? Links {
            get { return BackingStore?.Get<ResourceCollectionTopLevelLinks?>("links"); }
            set { BackingStore?.Set("links", value); }
        }
#nullable restore
#else
        public ResourceCollectionTopLevelLinks Links {
            get { return BackingStore?.Get<ResourceCollectionTopLevelLinks>("links"); }
            set { BackingStore?.Set("links", value); }
        }
#endif
        /// <summary>The meta property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public TagCollectionResponseDocument_meta? Meta {
            get { return BackingStore?.Get<TagCollectionResponseDocument_meta?>("meta"); }
            set { BackingStore?.Set("meta", value); }
        }
#nullable restore
#else
        public TagCollectionResponseDocument_meta Meta {
            get { return BackingStore?.Get<TagCollectionResponseDocument_meta>("meta"); }
            set { BackingStore?.Set("meta", value); }
        }
#endif
        /// <summary>
        /// Instantiates a new <see cref="TagCollectionResponseDocument"/> and sets the default values.
        /// </summary>
        public TagCollectionResponseDocument()
        {
            BackingStore = BackingStoreFactorySingleton.Instance.CreateBackingStore();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="TagCollectionResponseDocument"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static TagCollectionResponseDocument CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new TagCollectionResponseDocument();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"data", n => { Data = n.GetCollectionOfObjectValues<TagDataInResponse>(TagDataInResponse.CreateFromDiscriminatorValue)?.ToList(); } },
                {"included", n => { Included = n.GetCollectionOfObjectValues<DataInResponse>(DataInResponse.CreateFromDiscriminatorValue)?.ToList(); } },
                {"links", n => { Links = n.GetObjectValue<ResourceCollectionTopLevelLinks>(ResourceCollectionTopLevelLinks.CreateFromDiscriminatorValue); } },
                {"meta", n => { Meta = n.GetObjectValue<TagCollectionResponseDocument_meta>(TagCollectionResponseDocument_meta.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<TagDataInResponse>("data", Data);
            writer.WriteCollectionOfObjectValues<DataInResponse>("included", Included);
            writer.WriteObjectValue<ResourceCollectionTopLevelLinks>("links", Links);
            writer.WriteObjectValue<TagCollectionResponseDocument_meta>("meta", Meta);
        }
    }
}
