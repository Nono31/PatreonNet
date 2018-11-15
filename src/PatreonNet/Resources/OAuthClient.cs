using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// A client created by a developer, used for getting OAuth2 access tokens
    /// </summary>
    public class OAuthClient : PatreonObject
    {
        /// <summary>
        /// Secret token generated when you registered your client.
        /// </summary>
        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Display name for your app.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Details about client's functionality.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Author name for app attribution.
        /// </summary>
        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Company domain, e.g.www.patreon.com.
        /// </summary>
        [JsonProperty(PropertyName = "domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Client API version, e.g 2 for APIv2.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        /// <summary>
        /// Fully qualified url for your app icon, shown on oauth screen.
        /// </summary>
        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Fully qualified url to our privacy policy, linked on oauth screen.
        /// Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "privacy_policy_url")]
        public string PrivacyPolicyUrl { get; set; }

        /// <summary>
        /// Terms of Service url for your app. Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "tos_url")]
        public string TosUrl { get; set; }
        /// <summary>
        /// Space-separated list of fully qualified uris that your oauth is trusted to redirect to after connection.
        /// </summary>
        [JsonProperty(PropertyName = "redirect_uris")]
        public string RedirectUris { get; set; }

        /// <summary>
        /// Space-separated list of Patreon scopes you wish to associate with this oauth session.
        /// </summary>
        [JsonProperty(PropertyName = "default_scopes")]
        public string DefaultScopes { get; set; }


        #region Relationships

        /// <summary>
        /// User
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        /// <summary>
        /// Campaign
        /// </summary>
        [JsonProperty(PropertyName = "campaign")]
        public Campaign Campaign { get; set; }

        #endregion
    }
}
