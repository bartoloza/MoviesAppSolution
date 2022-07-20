using AppModels;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AggregatedTrailersController : ControllerBase
    {
        private readonly IAggregatedTrailersService _aggregatedTrailersService;
        private readonly ILogger _logger;

        public AggregatedTrailersController(IAggregatedTrailersService aggregatedTrailersService, ILogger<IMDBApiController> logger)
        {
            _aggregatedTrailersService = aggregatedTrailersService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<List<VideoTrailer>> GetAggregatedTrailers()
        {
            var trailers =  await _aggregatedTrailersService.GetAggregatedTrailers();
            return trailers;
        }

    }
}
