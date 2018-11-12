using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    public class Webhook : PatreonObject
    {
        /// <summary>
        /// List of events that will trigger this webhook.
        /// </summary>
        [JsonProperty(PropertyName = "triggers")]
        public List<object> Triggers { get; set; }

        /// <summary>
        /// Fully qualified uri where webhook will be sent(e.g.https://www.example.com/webhooks/incoming).
        /// </summary>
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// true if the webhook is paused as a result of repeated failed attempts to post to uri. Set to false to attempt to re-enable a previously failing webhook.
        /// </summary>
        [JsonProperty(PropertyName = "paused")]
        public bool Paused { get; set; }

        /// <summary>
        /// Last date that the webhook was attempted or used.
        /// </summary>
        [JsonProperty(PropertyName = "last_attempted_at")]
        public DateTimeOffset LastAttemptedAt { get; set; }

        /// <summary>
        /// Number of times the webhook has failed consecutively, when in an error state.
        /// </summary>
        [JsonProperty(PropertyName = "num_consecutive_times_failed")]
        public int NumConsecutiveTimesFailed { get; set; }

        /// <summary>
        /// Secret used to sign your webhook message body, so you can validate authenticity upon receipt.
        /// </summary>
        [JsonProperty(PropertyName = "secret")]
        public string Secret { get; set; }
    }
}