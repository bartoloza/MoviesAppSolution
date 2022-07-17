using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppModels;
using MoviesAPI.Services;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    { 
        private readonly IIMDBService _mdbService;
        private readonly ILogger _logger;


        public MoviesController(IIMDBService imdbService, ILogger<MoviesController> logger)
        {
            _mdbService = imdbService;
            _logger = logger;
        }

        [HttpGet("{movieName}")]
        public async Task<MoviceCollection> GetMoviesByName(string movieName)
        {
            MoviceCollection moviceCollection = await _mdbService.GetMoviesByNameAsync(movieName);
            return moviceCollection;
        }

        [HttpGet("Trailer/{movieId}")]
        public async Task<TrailerData> GetTrailerByMovieId(string movieId)
        {
            TrailerData moviceCollection = await _mdbService.GetTrailerByMovieIdAsync(movieId);
            return moviceCollection;   
        }

        [HttpGet("YoutubeTrailer/{movieId}")]
        public async Task<YouTubeTrailerData> GetYoutubeLinkdAsync(string movieId)
        {
            YouTubeTrailerData moviceCollection = await _mdbService.GetYoutubeLinkdAsync(movieId);
            return moviceCollection;
        }
    }
}
