using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
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
    }
}
