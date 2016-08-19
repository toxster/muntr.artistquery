using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Muntr.Server.Core;
using Muntr.Server.Interfaces;

namespace Muntr.Server.Controllers
{
    [Route("api/[controller]")]
    public class ArtistController: Controller 
    {
        private AppSettings _settings { get; set; }

        private readonly ILogger<ArtistController> _logger;
        private readonly IArtistQueryRepository _artistQueryRepository;

        public ArtistController(IArtistQueryRepository artistQueryRepository, 
            ILogger<ArtistController> logger, IOptions<AppSettings> settings) 
        {
            this._logger = logger;
            this._artistQueryRepository = artistQueryRepository;
            this._settings = settings.Value;
        }


        [HttpGet("{id}")]
        public IActionResult GetByMBID(string id) 
        {
            _logger.LogDebug("Getting by id: {0}", id);
            var item = _artistQueryRepository.Find(id);
            return new ObjectResult(item);
        }

    }
}