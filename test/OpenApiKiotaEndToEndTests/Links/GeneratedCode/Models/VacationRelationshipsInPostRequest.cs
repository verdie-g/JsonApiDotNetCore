// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenApiKiotaEndToEndTests.Links.GeneratedCode.Models {
    #pragma warning disable CS1591
    public class VacationRelationshipsInPostRequest : IBackedModel, IParsable 
    #pragma warning restore CS1591
    {
        /// <summary>The accommodation property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ToOneAccommodationInRequest? Accommodation {
            get { return BackingStore?.Get<ToOneAccommodationInRequest?>("accommodation"); }
            set { BackingStore?.Set("accommodation", value); }
        }
#nullable restore
#else
        public ToOneAccommodationInRequest Accommodation {
            get { return BackingStore?.Get<ToOneAccommodationInRequest>("accommodation"); }
            set { BackingStore?.Set("accommodation", value); }
        }
#endif
        /// <summary>Stores model information.</summary>
        public IBackingStore BackingStore { get; private set; }
        /// <summary>The excursions property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ToManyExcursionInRequest? Excursions {
            get { return BackingStore?.Get<ToManyExcursionInRequest?>("excursions"); }
            set { BackingStore?.Set("excursions", value); }
        }
#nullable restore
#else
        public ToManyExcursionInRequest Excursions {
            get { return BackingStore?.Get<ToManyExcursionInRequest>("excursions"); }
            set { BackingStore?.Set("excursions", value); }
        }
#endif
        /// <summary>The transport property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public NullableToOneTransportInRequest? Transport {
            get { return BackingStore?.Get<NullableToOneTransportInRequest?>("transport"); }
            set { BackingStore?.Set("transport", value); }
        }
#nullable restore
#else
        public NullableToOneTransportInRequest Transport {
            get { return BackingStore?.Get<NullableToOneTransportInRequest>("transport"); }
            set { BackingStore?.Set("transport", value); }
        }
#endif
        /// <summary>
        /// Instantiates a new <see cref="VacationRelationshipsInPostRequest"/> and sets the default values.
        /// </summary>
        public VacationRelationshipsInPostRequest()
        {
            BackingStore = BackingStoreFactorySingleton.Instance.CreateBackingStore();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="VacationRelationshipsInPostRequest"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static VacationRelationshipsInPostRequest CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new VacationRelationshipsInPostRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                {"accommodation", n => { Accommodation = n.GetObjectValue<ToOneAccommodationInRequest>(ToOneAccommodationInRequest.CreateFromDiscriminatorValue); } },
                {"excursions", n => { Excursions = n.GetObjectValue<ToManyExcursionInRequest>(ToManyExcursionInRequest.CreateFromDiscriminatorValue); } },
                {"transport", n => { Transport = n.GetObjectValue<NullableToOneTransportInRequest>(NullableToOneTransportInRequest.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<ToOneAccommodationInRequest>("accommodation", Accommodation);
            writer.WriteObjectValue<ToManyExcursionInRequest>("excursions", Excursions);
            writer.WriteObjectValue<NullableToOneTransportInRequest>("transport", Transport);
        }
    }
}
