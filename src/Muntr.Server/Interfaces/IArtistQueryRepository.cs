using System.Collections.Generic;
using Muntr.Server.Models;

namespace Muntr.Server.Interfaces
{
    public interface IArtistQueryRepository
    {
        ArtistQueryResponse Find(string key);
    }
}