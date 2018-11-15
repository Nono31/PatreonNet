using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JsonApiSerializer;
using Newtonsoft.Json;
using PatreonNet.Resources;

namespace PatreonNet
{
    internal static class HttpContentExtensions
    {
        internal static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json, new JsonApiSerializerSettings());
            return value;
        }
    }
}
