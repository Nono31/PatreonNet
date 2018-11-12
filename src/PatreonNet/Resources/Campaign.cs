using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    public class Campaign : PatreonObject
    {
        /// <summary>
        /// The creator's summary of their campaign.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// The type of content the creator is creating, as in "vanity is creating creation_name".
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "creation_name")]
        public string CreationName { get; set; }
        /// <summary>
        /// The thing which patrons are paying per, as in "vanity is making $1000 per pay_per_name".
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "pay_per_name")]
        public string PayPerName { get; set; }
        /// <summary>
        /// Pithy one-liner for this campaign, displayed on the creator page.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "one_liner")]
        public string OneLiner { get; set; }
        /// <summary>
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "main_video_embed")]
        public string MainVideoEmbed { get; set; }
        /// <summary>
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "main_video_url")]
        public string MainVideoUrl { get; set; }
        /// <summary>
        /// Banner image URL for the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        /// <summary>
        /// URL for the campaign's profile image.
        /// </summary>
        [JsonProperty(PropertyName = "image_small_url")]
        public string ImageSmallUrl { get; set; }
        /// <summary>
        /// URL for the video shown to patrons after they pledge to this campaign.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "thanks_video_url")]
        public string ThanksVideoUrl { get; set; }

        /// <summary>
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "thanks_embed")]
        public string ThanksEmbed { get; set; }
        /// <summary>
        /// Thank you message shown to patrons after they pledge to this campaign.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "thanks_msg")]
        public string ThanksMessage { get; set; }
        /// <summary>
        /// true if the campaign charges per month, false if the campaign charges per-post.
        /// </summary>
        [JsonProperty(PropertyName = "is_monthly")]
        public bool IsMonthly { get; set; }
        /// <summary>
        /// Whether this user has opted-in to rss feeds.
        /// </summary>
        [JsonProperty(PropertyName = "has_rss")]
        public bool HasRss { get; set; }
        /// <summary>
        /// Whether or not the creator has sent a one-time rss notification email.
        /// </summary>
        [JsonProperty(PropertyName = "has_sent_rss_notify")]
        public bool HasSentRssNotify { get; set; }
        /// <summary>
        /// The title of the campaigns rss feed.
        /// </summary>
        [JsonProperty(PropertyName = "rss_feed_title")]
        public string RssFeedTitle { get; set; }
        /// <summary>
        /// The url for the rss album artwork.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "rss_artwork_url")]
        public string RssArtworkUrl { get; set; }
        /// <summary>
        /// true if the creator has marked the campaign as containing nsfw content.
        /// </summary>
        [JsonProperty(PropertyName = "is_nsfw")]
        public bool IsNsfw { get; set; }
        /// <summary>
        /// true if the campaign charges upfront, false otherwise.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "is_charged_immediately")]
        public bool? IsChargedImmediately { get; set; }
        /// <summary>
        /// Datetime that the creator first began the campaign creation process. See published_at.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Datetime that the creator most recently published (made publicly visible) the campaign.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "published_at")]
        public DateTimeOffset? PublishedAt { get; set; }
        /// <summary>
        /// Relative (to patreon.com) URL for the pledge checkout flow for this campaign.
        /// </summary>
        [JsonProperty(PropertyName = "pledge_url")]
        public string PledgeUrl { get; set; }
        /// <summary>
        /// Number of patrons pledging to this creator.
        /// </summary>
        [JsonProperty(PropertyName = "patron_count")]
        public int PatronCount { get; set; }
        /// <summary>
        /// The ID of the external discord server that is linked to this campaign.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "discord_server_id")]
        public string DiscordServerId { get; set; }
        /// <summary>
        /// The ID of the Google Analytics tracker that the creator wants metrics to be sent to.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "google_analytics_id")]
        public string GoogleAnalyticsId { get; set; }
        /// <summary>
        /// Controls the visibility of the total earnings in the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "earnings_visibility")]
        public string EarningsVisibility { get; set; }
    }
}
