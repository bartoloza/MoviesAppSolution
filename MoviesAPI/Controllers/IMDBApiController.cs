using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppModels;
using MoviesAPI.Services;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MoviesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IMDBApiController : ControllerBase
    { 
        private readonly IIMDBService _mdbService;
        private readonly ILogger _logger;


        public IMDBApiController(IIMDBService imdbService, ILogger<IMDBApiController> logger)
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
    }
}
