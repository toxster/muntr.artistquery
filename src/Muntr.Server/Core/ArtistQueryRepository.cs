using System;
using Muntr.Business.ExternalAdapters;
using Nito.AsyncEx;
using System.Threading.Tasks;
using System.Linq;
using Muntr.Server.Interfaces;
using System.Collections.Generic;
using Muntr.Server.Models;
using Microsoft.Extensions.Options;

namespace Muntr.Server.Core
{
    public class ArtistQueryRepository : IArtistQueryRepository
    {
        
        private static MusicBrainzAdapter _mbAdapter;
        private static CoverArtArchiveAdapter _caaAdapter;
        private static WikipediaAdapter _wikiAdapter;
        
        private AppSettings _settings;

        public ArtistQueryRepository(IOptions<AppSettings> settings)
        {
            this._settings = settings.Value;
            Console.WriteLine("URL: " + this._settings.CoverArtArchiveEndpointURL);
        }

        public ArtistQueryResponse Find(string key)
        {
            // our return class that will be serialized to json 
            ArtistQueryResponse item = new ArtistQueryResponse();

            if (_mbAdapter == null) {
                _mbAdapter = new MusicBrainzAdapter(this._settings.MusicBrainzEndpointURL);
            } 

            if (_caaAdapter == null) {
                _caaAdapter = new CoverArtArchiveAdapter(this._settings.CoverArtArchiveEndpointURL);
            }

            if (_wikiAdapter == null) {
                _wikiAdapter = new WikipediaAdapter(this._settings.WikipediaEndpointURL);
            }
            
            // by using Nito library, exceptions arent swallowed as aggregateexception. We handle it straight away instead.
            var result = AsyncContext.Run(() => _mbAdapter.LookupAsync(key));

            // populate some metadata returned from MB
            item.mbid = (Guid)result.SelectToken("id");
            item.country = (String)result.SelectToken("country");
            item.type = (String)result.SelectToken("type");
            item.name = (String)result.SelectToken("name");
            
            // transform result into List<Album> for albums 
            var albums = result.SelectTokens("release-groups[*]").Where(s => String.Equals((string)s["primary-type"], "Album", StringComparison.CurrentCultureIgnoreCase))
                .Select(a => new Album()
                {
                    title = (string)a["title"],
                    id = (Guid)a["id"]
                }).ToList();

            // Query CoverArtArchive for covers

            var dictAlbums = new Dictionary<Guid, string>();
            var covers = AsyncContext.Run(() => Task.WhenAll(albums.Select(a => _caaAdapter.GetCoverArtURLAsync(a.id))));
            foreach (var f in covers)
            {
                
            }

            return item;
        }
    }
}