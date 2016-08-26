using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace Muntr.Business.ExternalAdapters
{
    /// there is a 3rd party lib for accessing MusicBrainz, however, its not tested with dotnetcore.
    public class MusicBrainzAdapter
    {
        private static string kEndpointURL;
        private static HttpClient _Client;
        private HttpClient httpClient
        {
            get
            {
                if (_Client == null)
                    _Client = new HttpClient();

                return _Client;
            }
        }

        public MusicBrainzAdapter(string endpointBaseUrl)
        {
            kEndpointURL = endpointBaseUrl;
        }

        public async Task<JObject> LookupAsync(string mbid)
        {

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(String.Format(kEndpointURL, mbid)),
                Method = HttpMethod.Get,
            };
            // by using a unique User-Agent string, we are throttled to 50req/s. 
            // If that limit hits we return HTTP 500 and the clients will have to handle this, since 500 is a retryable error.
            request.Headers.Add("User-Agent", "MuntrArtistQuery/1.0.0 ( tobias@muntr.com )");

            try
            {
                using (var r = await httpClient.SendAsync(request))
                {
                    r.EnsureSuccessStatusCode();
                    string result = await r.Content.ReadAsStringAsync();
                    return JObject.Parse(result);
                }
            }
            catch (HttpRequestException) // not found. This could be extended in checking resultcodes in a more proper way, and throwing custom Exception to act upon in Repository, but so little time...
            {
                return null;
            }


        }
    }
}