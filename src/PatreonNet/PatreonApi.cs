using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JsonApiSerializer;
using Newtonsoft.Json;
using PatreonNet.Resources;
using PatreonNet.Resources.OAuth;
using PatreonNet.Utils;

namespace PatreonNet
{

    public class PatreonApi
    {
        const string baseUrl = "https://www.patreon.com";
        const string baseApiUrl = "https://api.patreon.com";
        const string authorizationEndpoint = "oauth2/authorize";
        const string tokenEndpoint = "oauth2/token";

        private string _accessToken;

        private string _clientId;
        private string _clientSecret;
        private string _redirectURI;

        public PatreonApi()
        {
        }

        public PatreonApi(string clientId, string clientSecret, string redirectURI) : this()
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectURI = redirectURI;
        }

        public PatreonApi(string clientId, string clientSecret) : this()
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public PatreonApi(string accessToken) : this()
        {
            _accessToken = accessToken;
        }

        public static List<string> Scopes
        {
            get
            {
                return new List<string>()
                {
                    "identity",
                    "identity[email]",
                    "identity.memberships",
                    "campaigns",
                    "w:campaigns.webhook",
                    "campaigns.members",
                    "campaigns.members[email]",
                    "campaigns.members.address 	"
                };
            }
        }

        public Uri BuildAuthorizeEndpoint(List<string> scopes)
        {
            var s = string.Empty;
            if (scopes.Any())
                s = new StringBuilder().AppendJoin("%20", scopes).ToString();

            var state = GenerateState();
            var authorizationRequest =
                $"{authorizationEndpoint}?response_type=code&client_id={_clientId}&scope={s}&redirect_uri={_redirectURI}&state={state}";
            var uri = new Uri(new Uri(baseUrl), authorizationRequest);
            return uri;
        }

        private static string GenerateState()
        {
            return Guid.NewGuid().ToString("N");
        }


        public async Task<TokenResponse> PerformCodeExchange(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
                request.Content = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("client_id", _clientId),
                        new KeyValuePair<string, string>("client_secret", _clientSecret),
                        new KeyValuePair<string, string>("redirect_uri", _redirectURI)
                    });
                var response = await client.SendAsync(request);
                var token = JsonConvert.DeserializeObject<TokenResponse>(response.Content.ReadAsStringAsync()
                    .Result);
                return token;
            }
        }

        public async Task<TokenResponse> PerformTokenRefresh(string refreshToken)
        {
            HttpClient InnerClient = new HttpClient();
            InnerClient.BaseAddress = new Uri(baseApiUrl);
            var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            request.Content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret)
                });
            var response = await InnerClient.SendAsync(request);
            var result = JsonConvert.DeserializeObject<TokenResponse>(response.Content.ReadAsStringAsync()
                .Result);
            return result;
        }


        /// <summary>
        /// Fetches the current User resource.
        /// <see href="https://docs.patreon.com/#get-api-oauth2-v2-identity"/>
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<User> FetchUser(Fields fields = null, Includes includes = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = CreateEndpointWithQuery("/api/oauth2/v2/identity", fields, includes);

                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadAsJsonAsync<User>();
                    return user;
                }

            }

            return null;
        }
        /// <summary>
        /// Get a particular member by id.
        /// <see href="https://docs.patreon.com/#get-api-oauth2-v2-members-id"/>
        /// </summary>
        /// <remarks>Requires the campaigns.members scope.</remarks>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<Member> FetchMember(string id, Fields fields = null, Includes includes = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = CreateEndpointWithQuery($"/api/oauth2/v2/members/{id}", fields, includes);

                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var member = await response.Content.ReadAsJsonAsync<Member>();
                    return member;
                }

            }

            return null;
        }

        /// <summary>
        /// <see href="https://docs.patreon.com/#get-api-oauth2-v2-campaigns"/>
        /// </summary>
        /// <returns>Returns a list of Campaigns owned by the authorized user.</returns>
        /// <param name="fields"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<List<Campaign>> FetchCampaigns(Fields fields = null, Includes includes = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = CreateEndpointWithQuery("/api/oauth2/v2/campaigns", fields, includes);

                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var campaigns = await response.Content.ReadAsJsonAsync<List<Campaign>>();
                    return campaigns;
                }
            }

            return null;
        }


        /// <summary>
        /// <see href="https://docs.patreon.com/#get-api-oauth2-v2-campaigns-campaign_id"/>
        /// </summary>
        /// <returns>Teturn information about a single Campaign, fetched by campaign ID.</returns>
        /// <returns>Returns a list of Campaigns owned by the authorized user.</returns>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<Campaign> FetchCampaign(string id, Fields fields = null, Includes includes = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = CreateEndpointWithQuery($"/api/oauth2/v2/campaigns/{id}", fields, includes);

                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var campaign = await response.Content.ReadAsJsonAsync<Campaign>();
                    return campaign;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the Members for a given Campaign.
        /// <see href="https://docs.patreon.com/#get-api-oauth2-v2-campaigns-campaign_id-members"/>
        /// </summary>
        /// <remarks>Requires the campaigns.members scope.</remarks>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<List<Member>> FetchCampaignMembers(string id, Fields fields = null, Includes includes = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = CreateEndpointWithQuery($"/api/oauth2/v2/campaigns/{id}/members", fields, includes);

                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var members = await response.Content.ReadAsJsonAsync<List<Member>>();
                    return members;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the Webhooks for the current user's Campaign created by the API client. You will only be able to see webhooks created by your client.
        /// </summary>
        /// <remarks>Requires the w:campaigns.webhook scope.</remarks>
        /// <param name="fields"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<List<Webhook>> FetchWebhooks(Fields fields = null, Includes includes = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var endpoint = CreateEndpointWithQuery($"/api/oauth2/v2/webhooks", fields, includes);

                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var webhooks = await response.Content.ReadAsJsonAsync<List<Webhook>>();
                    return webhooks;
                }
            }

            return null;
        }

        public async Task<Webhook> CreateWebhooks(Webhook webhook)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(webhook, new JsonApiSerializerSettings());
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PostAsync($"/api/oauth2/v2/webhooks", content);
                if (response.IsSuccessStatusCode)
                {
                    var newWebhook = await response.Content.ReadAsJsonAsync<Webhook>();
                    return newWebhook;
                }
            }

            return null;
        }

        public async Task<Webhook> UpdateWebhooks(Webhook webhook)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://www.patreon.com");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(webhook, new JsonApiSerializerSettings());
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PatchAsync($"/api/oauth2/v2/webhooks/{webhook.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var newWebhook = await response.Content.ReadAsJsonAsync<Webhook>();
                    return newWebhook;
                }
            }

            return null;
        }



        private static string CreateEndpointWithQuery(string endpoint, Fields fields, Includes includes)
        {
            var uri = new StringBuilder(endpoint);
            if (fields != null && !fields.IsEmpty())
            {
                uri.Append(QueryHelpers.HasQuery(uri.ToString()) ? '&' : '?');
                uri.Append(fields.ToQueryParams());
            }

            if (includes != null && includes.Any())
            {
                uri.Append(QueryHelpers.HasQuery(uri.ToString()) ? '&' : '?');
                uri.Append(includes.ToQueryParams());
            }

            return uri.ToString();
        }
    }
}
