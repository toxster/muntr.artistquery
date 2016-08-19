using System;
using System.Collections.Generic;

namespace Muntr.Server.Models
{

    public class Album {
        public string title { get; set; }
        public Guid id { get; set; }
        public string image { get; set; }
        public string thumbsmall { get; set; }
        public string thumblarge { get; set; }
        
    }

    public class ArtistQueryResponse
    {
        public Guid mbid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string country { get; set; }
        public string description { get; set; } // source: Wikipeda
        public List<Album> albums { get; set; } // img-src: CoverArtArchive

        public ArtistQueryResponse() {
            this.albums = new List<Album>();
        }
    }
}