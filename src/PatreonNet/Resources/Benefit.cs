using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    public class Benefit : PatreonObject
    {
        /// <summary>
        /// Benefit display title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        /// <summary>
        /// Benefit display description
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        /// <summary>
        /// Type of benefit, such as custom for creator-defined benefits.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "benefit_type")]
        public string BenefitType { get; set; }
        /// <summary>
        ///A rule type designation, such as eom_monthly or one_time_immediate.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "rule_type")]
        public string RuleType { get; set; }
        /// <summary>
        /// Datetime this benefit was created.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Number of deliverables for this benefit that have been marked complete.
        /// </summary>
        [JsonProperty(PropertyName = "delivered_deliverables_count")]
        public int DeliveredDeliverablesCount { get; set; }
        /// <summary>
        /// Number of deliverables for this benefit that are due, for all dates.
        /// </summary>
        [JsonProperty(PropertyName = "not_delivered_deliverables_count")]
        public int NotDeliveredDeliverablesCount { get; set; }
        /// <summary>
        /// Number of deliverables for this benefit that are due today specifically.
        /// </summary>
        [JsonProperty(PropertyName = "deliverables_due_today_count")]
        public int DeliverablesDueTodayCount { get; set; }
        /// <summary>
        /// The next due date (after EOD today) for this benefit. Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "next_deliverable_due_date")]
        public DateTimeOffset? NextDeliverableDueDate { get; set; }
        /// <summary>
        /// Number of tiers containing this benefit.
        /// </summary>
        [JsonProperty(PropertyName = "tiers_count")]
        public int TiersCount { get; set; }
        /// <summary>
        /// true if this benefit has been deleted.
        /// </summary>
        [JsonProperty(PropertyName = "is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
