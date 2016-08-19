using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace Muntr.Business.ExternalAdapters
{
    /// there is a 3rd party lib for accessing MusicBrainz, however, its not tested with dotnetcore.
    public class CoverArtArchiveAdapter {
        //kMusicBrainzEndointURL = ConfigurationManager.AppSettings["MusicBrainzEndpointURL"];
        private static string kEndpointURL;
        
        public CoverArtArchiveAdapter(string endpointBaseUrl) {
            kEndpointURL = endpointBaseUrl;
        }

        private static HttpClient _Client;
        private HttpClient httpClient { get {
            if (_Client == null)
                _Client = new HttpClient();
                
            return _Client;
        }}
        /// Returns: Tuple of Guid, front img, thumb-large, thumb-small uris.
        public async Task<Tuple<Guid, string, string, string>> GetCoverArtURLAsync(Guid mbid) {
            var request = new HttpRequestMessage() {
                RequestUri = new Uri(String.Format(kEndpointURL, mbid.ToString().ToLower())),
                Method = HttpMethod.Get,
            };

            request.Headers.Add("User-Agent", "MuntrArtistQuery/1.0.0 ( tobias@muntr.com )");
            request.Headers.Add("Accept", "application/json; UTF8");


            try
            {
                using (var r = await httpClient.SendAsync(request))
                {
                    r.EnsureSuccessStatusCode();
                    string result = await r.Content.ReadAsStringAsync();

                    JObject parsedResult = JObject.Parse(result);
                    return Tuple.Create(mbid, (string)parsedResult.SelectToken("images[0].image"),
                        (string)parsedResult.SelectToken("images[0].thumbnails.large"),
                        (string)parsedResult.SelectToken("images[0].thumbnails.small"));
                }
            }
            catch (HttpRequestException)
            {
                // cover art not found.
                return Tuple.Create<Guid, string, string, string>(mbid, null, null, null);
            }


        } 
    }
}