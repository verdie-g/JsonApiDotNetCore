// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenApiKiotaEndToEndTests.ClientIdGenerationModes.GeneratedCode.Models {
    public class PlayerGroups : DataInResponse, IParsable {
        /// <summary>The attributes property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public PlayerGroupAttributesInResponse? Attributes {
            get { return BackingStore?.Get<PlayerGroupAttributesInResponse?>("attributes"); }
            set { BackingStore?.Set("attributes", value); }
        }
#nullable restore
#else
        public PlayerGroupAttributesInResponse Attributes {
            get { return BackingStore?.Get<PlayerGroupAttributesInResponse>("attributes"); }
            set { BackingStore?.Set("attributes", value); }
        }
#endif
        /// <summary>The links property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public LinksInResourceData? Links {
            get { return BackingStore?.Get<LinksInResourceData?>("links"); }
            set { BackingStore?.Set("links", value); }
        }
#nullable restore
#else
        public LinksInResourceData Links {
            get { return BackingStore?.Get<LinksInResourceData>("links"); }
            set { BackingStore?.Set("links", value); }
        }
#endif
        /// <summary>The meta property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public PlayerGroups_meta? Meta {
            get { return BackingStore?.Get<PlayerGroups_meta?>("meta"); }
            set { BackingStore?.Set("meta", value); }
        }
#nullable restore
#else
        public PlayerGroups_meta Meta {
            get { return BackingStore?.Get<PlayerGroups_meta>("meta"); }
            set { BackingStore?.Set("meta", value); }
        }
#endif
        /// <summary>The relationships property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public PlayerGroupRelationshipsInResponse? Relationships {
            get { return BackingStore?.Get<PlayerGroupRelationshipsInResponse?>("relationships"); }
            set { BackingStore?.Set("relationships", value); }
        }
#nullable restore
#else
        public PlayerGroupRelationshipsInResponse Relationships {
            get { return BackingStore?.Get<PlayerGroupRelationshipsInResponse>("relationships"); }
            set { BackingStore?.Set("relationships", value); }
        }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new PlayerGroups CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new PlayerGroups();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers()) {
                {"attributes", n => { Attributes = n.GetObjectValue<PlayerGroupAttributesInResponse>(PlayerGroupAttributesInResponse.CreateFromDiscriminatorValue); } },
                {"links", n => { Links = n.GetObjectValue<LinksInResourceData>(LinksInResourceData.CreateFromDiscriminatorValue); } },
                {"meta", n => { Meta = n.GetObjectValue<PlayerGroups_meta>(PlayerGroups_meta.CreateFromDiscriminatorValue); } },
                {"relationships", n => { Relationships = n.GetObjectValue<PlayerGroupRelationshipsInResponse>(PlayerGroupRelationshipsInResponse.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public override void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            base.Serialize(writer);
            writer.WriteObjectValue<PlayerGroupAttributesInResponse>("attributes", Attributes);
            writer.WriteObjectValue<LinksInResourceData>("links", Links);
            writer.WriteObjectValue<PlayerGroups_meta>("meta", Meta);
            writer.WriteObjectValue<PlayerGroupRelationshipsInResponse>("relationships", Relationships);
        }
    }
}