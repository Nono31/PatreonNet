using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// The record of a user's membership to a campaign. Remains consistent across months of pledging.
    /// </summary>
    public class Member : PatreonObject
    {
        /// <summary>
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "patron_status")]
        public string PatronStatus { get; set; }
        /// <summary>
        /// The user is not a pledging patron but has subscribed to updates about public posts.
        /// </summary>
        [JsonProperty(PropertyName = "is_follower")]
        public bool IsFollower { get; set; }

        /// <summary>
        /// Full name of the member user.
        /// </summary>
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// The member's email address.
        /// </summary>
        /// <remarks>Requires the campaigns.members[email] scope.</remarks>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Datetime of beginning of most recent pledge chainfrom this member to the campaign.Pledge updates do not change this value.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "pledge_relationship_start")]
        public DateTimeOffset? PledgeRelationshipStart { get; set; }
        
        /// <summary>
        /// The total amount that the member has ever paid to the campaign.
        /// </summary>
        /// <remarks>0 if never paid.</remarks>
        [JsonProperty(PropertyName = "lifetime_support_cents")]
        public int LifetimeSupportCents { get; set; }

        /// <summary>
        /// The amount in cents that the member is entitled to.
        /// This includes a current pledge, or payment that covers the current payment period.
        /// </summary>
        [JsonProperty(PropertyName = "currently_entitled_amount_cents")]
        public int CurrentlyEntitledAmountCents { get; set; }

        /// <summary>
        /// Datetime of last attempted charge. null if never charged.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "last_charge_date")]
        public DateTimeOffset? LastChargeDate { get; set; }

        /// <summary>
        /// The result of the last attempted charge.
        /// Possible values are['Paid', 'Declined', 'Deleted', 'Pending', 'Refunded', 'Fraud', 'Other', null].
        /// The only successful status is Paid. null if never charged.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "last_charge_status")]
        public string LastChargeStatus { get; set; }

        /// <summary>
        /// The creator's notes on the member.
        /// </summary>
        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }

        /// <summary>
        /// The amount in cents the user will pay at the next pay cycle.
        /// </summary>
        [JsonProperty(PropertyName = "will_pay_amount_cents")]
        public int WillPayAmountCents { get; set; }

        #region Relationships

        /// <summary>
        /// The member's shipping address that they entered for the campaign. Requires the campaign.members.address scope.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// The campaign that the membership is for.
        /// </summary>
        [JsonProperty(PropertyName = "campaign")]
        public Campaign Campaign { get; set; }

        /// <summary>
        /// The tiers that the member is entitled to. This includes a current pledge, or payment that covers the current payment period.
        /// </summary>
        [JsonProperty(PropertyName = "currently_entitled_tiers")]
        public IList<Tier> CurrentlyEntitledTiers { get; set; }

        /// <summary>
        /// The user who is pledging to the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        #endregion
    }
}
