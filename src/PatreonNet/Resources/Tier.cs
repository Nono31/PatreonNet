using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// A membership level on a campaign, which can have benefits attached to it.
    /// </summary>
    public class Tier : PatreonObject
    {
        /// <summary>
        /// Monetary amount associated with this tier(in U.S.cents).
        /// </summary>
        [JsonProperty(PropertyName = "amount_cents")]
        public int AmountCents { get; set; }

        /// <summary>
        /// Maximum number of patrons this tier is limited to, if applicable.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "user_limit")]
        public int? UserLimit { get; set; }
        /// <summary>
        /// 
        /// Remaining number of patrons who may subscribe, if there is a user_limit.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "remaining")]
        public int? Remaining { get; set; }

        /// <summary>
        /// Tier display description.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// true if this tier requires a shipping address from patrons.
        /// </summary>
        [JsonProperty(PropertyName = "requires_shipping")]
        public bool RequiresShipping { get; set; }

        /// <summary>
        /// Datetime this tier was created.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Fully qualified URL associated with this tier.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Number of patrons currently registered for this tier.
        /// </summary>
        [JsonProperty(PropertyName = "patron_count")]
        public int PatronCount { get; set; }

        /// <summary>
        /// Number of posts published to this tier.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "post_count")]
        public int? PostCount { get; set; }

        /// <summary>
        /// The discord role IDs granted by this tier.Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "discord_role_ids")]
        public string DiscordRoleIds { get; set; }

        /// <summary>
        /// Tier display title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Full qualified image URL associated with this tier.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        /// <summary>
        /// Datetime tier was last modified.
        /// </summary>
        [JsonProperty(PropertyName = "edited_at")]
        public DateTimeOffset EditedAt { get; set; }

        /// <summary>
        /// true if the tier is currently published.
        /// </summary>
        [JsonProperty(PropertyName = "published")]
        public bool Published { get; set; }

        /// <summary>
        /// Datetime this tier was last published.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "published_at")]
        public DateTimeOffset? PublishedAt { get; set; }

        /// <summary>
        /// Datetime tier was unpublished, while applicable. Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "unpublished_at")]
        public DateTimeOffset? UnpublishedAt { get; set; }


        #region Relationships

        /// <summary>
        /// The campaign the tier belongs to.
        /// </summary>
        [JsonProperty(PropertyName = "campaign")]
        public Campaign Campaign { get; set; }

        /// <summary>
        /// The image file associated with the tier.
        /// </summary>
        [JsonProperty(PropertyName = "tier_image")]
        public Media TierImage { get; set; }

        /// <summary>
        /// The benefits attached to the tier, which are used for generating deliverables
        /// </summary>
        [JsonProperty(PropertyName = "benefits")]
        public IList<Benefit> Benefits { get; set; }

        #endregion
    }
}
