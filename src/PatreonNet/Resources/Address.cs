using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// A User's shipping address.
    /// </summary>
    public class Address : PatreonObject
    {

        /// <summary>
        /// Full recipient name.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "addressee")]
        public string Addressee { get; set; }
        /// <summary>
        /// First line of street address.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "line_1")]
        public string Line1 { get; set; }
        /// <summary>
        /// Second line of street address.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "line_2")]
        public string Line2 { get; set; }
        /// <summary>
        /// Postal or zip code.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }
        /// <summary>
        /// City.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        /// <summary>
        /// State or province name.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        /// <summary>
        /// Country.
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        /// <summary>
        /// Telephone number. Specified for non-US addresses.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Datetime address was first created.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// true if the address was confirmed after creation.
        /// </summary>
        [JsonProperty(PropertyName = "confirmed")]
        public bool Confirmed { get; set; }

        /// <summary>
        /// When this address was last confirmed, set by confirmed action attribute.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "confirmed_at")]
        public DateTimeOffset? ConfirmedAt { get; set; }

        #region Relationships
        /// <summary>
        /// User
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        /// <summary>
        /// campaigns
        /// </summary>
        [JsonProperty(PropertyName = "campaigns")]
        public IList<Campaign> Campaigns { get; set; }
        #endregion

    }
}
