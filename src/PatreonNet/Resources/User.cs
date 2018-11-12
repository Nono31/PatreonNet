using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    public class User : PatreonObject
    {
        /// <summary>
        /// The user's email address.
        /// Requires certain scopes to access.
        /// See the scopes section of this documentation.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// First name.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Combined first and last name.
        /// </summary>
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// true if the user has confirmed their email.
        /// Requires certain scopes to access.See the scopes section of this documentation.
        /// </summary>
        [JsonProperty(PropertyName = "is_email_verified")]
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// The public "username" of the user.patreon.com/ goes to this user's creator page.
        /// Non-creator users might not have a vanity.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "vanity")]
        public string Vanity { get; set; }

        /// <summary>
        /// The user's about text, which appears on their profile.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "about")]
        public string About { get; set; }

        /// <summary>
        /// The user's profile picture URL, scaled to width 400px.
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// The user's profile picture URL, scaled to a square of size 100x100px.
        /// </summary>
        [JsonProperty(PropertyName = "thumb_url")]
        public string ThumbUrl { get; set; }

        /// <summary>
        /// true if this user can view nsfw content.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "can_see_nsfw")]
        public bool? CanSeeNsfw { get; set; }

        /// <summary>
        /// Datetime of this user's account creation.
        /// </summary>
        /// <remarks>Why not created_at</remarks>
        [JsonProperty(PropertyName = "created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// URL of this user's creator or patron profile.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// How many posts this user has liked.
        /// </summary>
        [JsonProperty(PropertyName = "like_count")]
        public int LikeCount { get; set; }

        /// <summary>
        /// true if the user has chosen to keep private which creators they pledge to.Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "hide_pledges")]
        public bool HidePledges { get; set; }

        /// <summary>
        /// Mapping from user's connected app names to an object containing the external user id and a profile URL in the respective app.
        /// </summary>
        [JsonProperty(PropertyName = "social_connections")]
        public object SocialConnections { get; set; }
    }
}
