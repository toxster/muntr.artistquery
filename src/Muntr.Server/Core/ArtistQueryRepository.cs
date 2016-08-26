using System;
using Muntr.Business.ExternalAdapters;
using Nito.AsyncEx;
using System.Threading.Tasks;
using System.Linq;
using Muntr.Server.Interfaces;
using System.Collections.Generic;
using Muntr.Server.Models;
using Microsoft.Extensions.Options;
using Muntr.Business.Misc;
using System.Net.Http;

namespace Muntr.Server.Core
{
    public class ArtistQueryRepository : IArtistQueryRepository
    {

        private static MusicBrainzAdapter _mbAdapter;
        private static CoverArtArchiveAdapter _caaAdapter;
        private static WikipediaAdapter _wikiAdapter;

        private static HttpClient _httpClient;

        private AppSettings _settings;

        public ArtistQueryRepository(IOptions<AppSettings> settings, HttpClient httpClient = null)
        {
            // this injects our mocked httpclient from testing.
            if (httpClient == null) {
                _httpClient = new HttpClient();
            } else {
                _httpClient = httpClient;
            }
            
            this._settings = settings.Value;
            Console.WriteLine("URL: " + this._settings.CoverArtArchiveEndpointURL);
        }

        public ArtistQueryResponse Find(string key)
        {
            // our return class that will be serialized to json 
            ArtistQueryResponse item = new ArtistQueryResponse();

            if (_mbAdapter == null)
            {
                _mbAdapter = new MusicBrainzAdapter(this._settings.MusicBrainzEndpointURL);
            }

            if (_caaAdapter == null)
            {
                _caaAdapter = new CoverArtArchiveAdapter(this._settings.CoverArtArchiveEndpointURL);
            }

            if (_wikiAdapter == null)
            {
                _wikiAdapter = new WikipediaAdapter(this._settings.WikipediaEndpointURL);
            }

            // by using Nito library, exceptions arent swallowed as aggregateexception. We handle it straight away instead.
            var result = AsyncContext.Run(() => _mbAdapter.LookupAsync(key));

            // populate some metadata returned from MB
            item.mbid = (Guid)result.SelectToken("id");
            item.country = (String)result.SelectToken("country");
            item.type = (String)result.SelectToken("type");
            item.name = (String)result.SelectToken("name");


            // Get wiki stub name, so we can fetch description
            var wikiRelation = result.SelectTokens("relations[*]").Where(s => String.Equals((string)s["type"], "wikipedia", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (wikiRelation != null)
            {
                var stubName = (string)wikiRelation.SelectToken("url.resource");
                item.description = AsyncContext.Run(() => _wikiAdapter.GetDescriptionAsync(HelpUtils.GetWikipediaArticleStub(stubName)));
            }

            // grab albums from MusicBrainz to lookup against CoverArtArchive
            var albums = result.SelectTokens("release-groups[*]").Where(s => String.Equals((string)s["primary-type"], "Album", StringComparison.CurrentCultureIgnoreCase))
                .Select(a => new
                {
                    id = (Guid)a["id"],
                    title = (string)a["title"]
                });
            // put it in a dictionay for near O(1) performance when iterating below.
            Dictionary<Guid, string> dictAlbums = albums.ToDictionary(k => k.id, e => e.title);

            // Query CoverArtArchive for covers
            var covers = AsyncContext.Run(() => Task.WhenAll(dictAlbums.Select(a => _caaAdapter.GetCoverArtURLAsync(a.Key))));
            foreach (var f in covers)
            {
                item.albums.Add(new Album
                {
                    id = f.Item1,
                    title = dictAlbums[f.Item1],
                    image = f.Item2,
                    thumblarge = f.Item3,
                    thumbsmall = f.Item4
                });
            }
            return item;
        }
    }
}