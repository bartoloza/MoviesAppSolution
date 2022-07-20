using AppModels;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class YoutubeApiController : ControllerBase
    {
        private readonly IYoutubeService _youtubeService;
        private readonly ILogger _logger;

        public YoutubeApiController(IYoutubeService youtubeService, ILogger<IMDBApiController> logger)
        {
            _youtubeService = youtubeService;
            _logger = logger;
        }

        [HttpGet("Trailer/{movieName}")]
        public async Task<YoutubeTrailerResponse> GetMoviesByNameAsync(string movieName)
        {
            YoutubeTrailerResponse moviceCollection = await _youtubeService.GetTrailerByMovieNameAsync(movieName);
            _logger.LogError("YOUTUBE RESPONSE LOG: " + moviceCollection.ToString());
            return moviceCollection;
        }

    }
}
