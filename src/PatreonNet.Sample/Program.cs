using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Net.Http.Server;
using PatreonNet.Resources;

namespace PatreonNet.Sample
{
    class Program
    {
        const string clientId = "<ClientId>";
        const string clientSecret = "<ClientSecret>";
        const string redirectURI = "http://localhost:3000/";

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var code = await Authorize();
            var patreonApi = new PatreonApi(clientId, clientSecret, redirectURI);
            var tokenData = await patreonApi.PerformCodeExchange(code);
            patreonApi = new PatreonApi(tokenData.AccessToken);

            var fields = new Fields();
            fields.AddAllField<User>();
            fields.AddAllField<Campaign>();
            var includes = new Includes();
            includes.Add<User>(x => x.Campaign);

            var user = await patreonApi.FetchUser(fields, includes);
            Console.ReadLine();
        }

        private static async Task<string> Authorize()
        {
            var patreonApi = new PatreonApi(clientId, clientSecret, redirectURI);

            var settings = new WebListenerSettings();
            settings.UrlPrefixes.Add("http://localhost:3000");

            var uri =
                patreonApi.BuildAuthorizeEndpoint(new List<string>( ) { "identity", "identity.memberships", "campaigns", "campaigns.members"});

            string queryString = string.Empty;

            using (WebListener listener = new WebListener(settings))
            {
                listener.Start();

                // Opens request in the browser.
                OpenBrowser(uri.ToString());

                using (var context = await listener.AcceptAsync())
                {
                    byte[] bytes = Encoding.ASCII.GetBytes("<html><head><meta http-equiv=\'refresh\'></head><body>Success</body></html>");
                    context.Response.ContentLength = bytes.Length;
                    context.Response.ContentType = "text/html";

                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    queryString = context.Request.QueryString;
                }
            }

            var queryDictionary = HttpUtility.ParseQueryString(queryString);

            // Checks for errors.
            if (queryDictionary.GetValues("error")?.FirstOrDefault() != null)
            {
                return string.Empty;
            }
            if (queryDictionary.GetValues("code")?.FirstOrDefault() == null)
            {
                return string.Empty;
            }

            // extracts the code
            return queryDictionary.GetValues("code").FirstOrDefault();
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
