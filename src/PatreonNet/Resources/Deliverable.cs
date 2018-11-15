using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// The record of whether or not a patron has been delivered the benefitthey are owed because of their member tier.
    /// </summary>
    public class Deliverable : PatreonObject
    {
        /// <summary>
        /// When the creator marked the deliverable as completed or fulfilled to the patron.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "completed_at")]
        public DateTimeOffset? CompletedAt { get; set; }

        /// <summary>
        /// One of 'not_delivered', 'delivered', 'wont_deliver'.
        /// </summary>
        [JsonProperty(PropertyName = "delivery_status")]
        public string DeliveryStatus { get; set; }

        /// <summary>
        /// When the deliverable is due to the patron.
        /// </summary>
        [JsonProperty(PropertyName = "due_at")]
        public DateTimeOffset? DueAt { get; set; }


        #region Relationships

        /// <summary>
        /// Campaign
        /// </summary>
        [JsonProperty(PropertyName = "campaign")]
        public Campaign Campaign { get; set; }

        /// <summary>
        /// Benefit
        /// </summary>
        [JsonProperty(PropertyName = "benefit")]
        public Benefit Benefit { get; set; }

        /// <summary>
        /// The member who has been granted the deliverable.
        /// </summary>
        [JsonProperty(PropertyName = "member")]
        public Member Member { get; set; }

        /// <summary>
        /// The user who has been granted the deliverable.This user is the same as the member user.
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        #endregion
    }
}
