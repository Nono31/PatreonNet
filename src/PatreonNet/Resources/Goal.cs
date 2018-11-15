using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// A funding goal in USD set by a creator on a campaign.
    /// </summary>
    public class Goal : PatreonObject
    {
        /// <summary>
        /// Goal amount in USD cents.
        /// </summary>
        [JsonProperty(PropertyName = "amount_cents")]
        public int AmountCents { get; set; }

        /// <summary>
        /// Goal title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Goal description.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// When the goal was created for the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When the campaign reached the goal.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "reached_at")]
        public string ReachedAt { get; set; }

        /// <summary>
        /// Equal to (pledge_sum/goal amount)*100, helpful when a creator
        /// </summary>
        [JsonProperty(PropertyName = "completed_percentage")]
        public int CompletedPercentage { get; set; }


        #region Relationships

        /// <summary>
        /// The campaign trying to reach the goal
        /// </summary>
        [JsonProperty(PropertyName = "campaign")]
        public Campaign Campaign { get; set; }

        #endregion
    }
}
