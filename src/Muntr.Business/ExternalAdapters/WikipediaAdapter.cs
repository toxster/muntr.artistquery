using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;


namespace Muntr.Business.ExternalAdapters
{
    public class WikipediaAdapter
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
        public WikipediaAdapter(string endpointBaseUrl)
        {
            kEndpointURL = endpointBaseUrl;
        }

        public async Task<string> GetDescriptionAsync(string articleStub)
        {

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(String.Format(kEndpointURL, articleStub)),
                Method = HttpMethod.Get
            };
            // No rate limits on Wikipedia, please read https://www.mediawiki.org/wiki/API:Etiquette for more information.
            request.Headers.Add("Accept", "application/json; UTF8");
            using (var r = await httpClient.SendAsync(request))
            {
                string result = await r.Content.ReadAsStringAsync();
                var parsedResult = JObject.Parse(result);
                var wikiPage = parsedResult.SelectTokens("query.pages.*").FirstOrDefault();
                if (wikiPage != null)
                {
                    return (string)wikiPage["extract"];
                }
                else
                {
                    return "";
                }
            }
        }
    }
}