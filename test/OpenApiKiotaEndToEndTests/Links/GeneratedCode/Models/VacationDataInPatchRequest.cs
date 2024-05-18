// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenApiKiotaEndToEndTests.Links.GeneratedCode.Models {
    #pragma warning disable CS1591
    public class VacationDataInPatchRequest : IBackedModel, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>The attributes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public VacationAttributesInPatchRequest? Attributes {
            get { return BackingStore?.Get<VacationAttributesInPatchRequest?>("attributes"); }
            set { BackingStore?.Set("attributes", value); }
        }
#nullable restore
#else
        public VacationAttributesInPatchRequest Attributes {
            get { return BackingStore?.Get<VacationAttributesInPatchRequest>("attributes"); }
            set { BackingStore?.Set("attributes", value); }
        }
#endif
        /// <summary>Stores model information.</summary>
        public IBackingStore BackingStore { get; private set; }
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id {
            get { return BackingStore?.Get<string?>("id"); }
            set { BackingStore?.Set("id", value); }
        }
#nullable restore
#else
        public string Id {
            get { return BackingStore?.Get<string>("id"); }
            set { BackingStore?.Set("id", value); }
        }
#endif
        /// <summary>The relationships property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public VacationRelationshipsInPatchRequest? Relationships {
            get { return BackingStore?.Get<VacationRelationshipsInPatchRequest?>("relationships"); }
            set { BackingStore?.Set("relationships", value); }
        }
#nullable restore
#else
        public VacationRelationshipsInPatchRequest Relationships {
            get { return BackingStore?.Get<VacationRelationshipsInPatchRequest>("relationships"); }
            set { BackingStore?.Set("relationships", value); }
        }
#endif
        /// <summary>The type property</summary>
        public VacationResourceType? Type {
            get { return BackingStore?.Get<VacationResourceType?>("type"); }
            set { BackingStore?.Set("type", value); }
        }
        /// <summary>
        /// Instantiates a new <see cref="VacationDataInPatchRequest"/> and sets the default values.
        /// </summary>
        public VacationDataInPatchRequest()
        {
            BackingStore = BackingStoreFactorySingleton.Instance.CreateBackingStore();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="VacationDataInPatchRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static VacationDataInPatchRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new VacationDataInPatchRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"attributes", n => { Attributes = n.GetObjectValue<VacationAttributesInPatchRequest>(VacationAttributesInPatchRequest.CreateFromDiscriminatorValue); } },
                {"id", n => { Id = n.GetStringValue(); } },
                {"relationships", n => { Relationships = n.GetObjectValue<VacationRelationshipsInPatchRequest>(VacationRelationshipsInPatchRequest.CreateFromDiscriminatorValue); } },
                {"type", n => { Type = n.GetEnumValue<VacationResourceType>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<VacationAttributesInPatchRequest>("attributes", Attributes);
            writer.WriteStringValue("id", Id);
            writer.WriteObjectValue<VacationRelationshipsInPatchRequest>("relationships", Relationships);
            writer.WriteEnumValue<VacationResourceType>("type", Type);
        }
    }
}
