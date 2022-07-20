using AppModels;
using Blazored.Toast.Services;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace MoviesAPI.Services
{
    public class IMDBService : IIMDBService
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger _logger;

        public IMDBService(IConfiguration configuration, IMemoryCache memoryCache, ILogger<IMDBService> logger)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<MoviceCollection> GetMoviesByNameAsync(string movieName)
        {
            MoviceCollection checkCached;

            checkCached = _memoryCache.Get<MoviceCollection>(movieName+"-imdb");

            if (checkCached is null)
            {
                using (var client = new HttpClient())
                {
                    var url = _configuration.GetValue<string>("urlIMDBSearchMovie") + _configuration.GetValue<string>("APIKeyIMDB") + "/" + movieName;
                    _logger.LogInformation("IMDB GetMoviesByNameAsync REQUEST  URL: \n" + url);

                    var response = await client.GetAsync(url);
                    var responseResults = response.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation("IMDB GetMoviesByNameAsync RESPONSE BODY: \n" + responseResults.ToString());


                    checkCached = JsonConvert.DeserializeObject<MoviceCollection>(responseResults);

                    _memoryCache.Set(movieName+"-imdb", checkCached, TimeSpan.FromSeconds(_configuration.GetValue<int>("CacheDuration")));
                }
            }
            return checkCached;
        }

        public async Task<TrailerData> GetTrailerByMovieIdAsync(string movieId)
        {
            TrailerData checkCached;
            checkCached = _memoryCache.Get<TrailerData>(movieId);

            if (checkCached is null)
            {
                using (var client = new HttpClient())
                {
                    var url = _configuration.GetValue<string>("urlIMDBTrailer") + _configuration.GetValue<string>("APIKeyIMDB") + "/" + movieId;
                    _logger.LogInformation("IMDB GetTrailerByMovieIdAsync RESPONSE BODY: \n" + url);

                    var response = await client.GetAsync(url);
                    var responseResults = response.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation("IMDB GetTrailerByMovieIdAsync RESPONSE BODY: \n" + responseResults.ToString());

                    checkCached = JsonConvert.DeserializeObject<TrailerData>(responseResults);

                    if (checkCached.LinkEmbed != null)
                    {
                        VideoTrailer videoTrailerCache = IMDBParser(checkCached);
                    }

                    _memoryCache.Set(movieId, checkCached, TimeSpan.FromSeconds(_configuration.GetValue<int>("CacheDuration")));
                }
            }
            return checkCached;

        }

        public VideoTrailer IMDBParser(TrailerData trailerData)
        {
            VideoTrailer videoTrailerCache = new VideoTrailer();
            if (trailerData is not null)
            {
                videoTrailerCache.Title = trailerData.FullTitle;
                videoTrailerCache.Id = trailerData.VideoId;
                videoTrailerCache.VIdeoUrl = trailerData.LinkEmbed;
                videoTrailerCache.ImageUrl = trailerData.ThumbnailUrl;

                _memoryCache.Set(trailerData.Title + "-imdbCache", videoTrailerCache, TimeSpan.FromSeconds(_configuration.GetValue<int>("CacheDuration")));
                VideoTrailerCacheAbstract.VideoTrailerCaches.Add(trailerData.FullTitle + "-imdbCache");
                VideoTrailerCollection.VideoTrailerCollectionList.Add(videoTrailerCache);

            }
            return videoTrailerCache;
        }
    }
}
