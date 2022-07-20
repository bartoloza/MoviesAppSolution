using AppModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace MoviesAPI.Services
{
    public class YoutubeService : IYoutubeService
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger _logger;

        public YoutubeService(IConfiguration configuration, IMemoryCache memoryCache, ILogger<YoutubeService> logger)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<YoutubeTrailerResponse> GetTrailerByMovieNameAsync(string movieName)
        {
            YoutubeTrailerResponse checkCached;

            checkCached = _memoryCache.Get<YoutubeTrailerResponse>(movieName+"-ytb");

            if (checkCached is null)
            {
                using (var client = new HttpClient())
                {
                    var url = _configuration.GetValue<string>("urlYouTube") + movieName + _configuration.GetValue<string>("urlYouTubeOfficialTrailer") + _configuration.GetValue<string>("APIKeyYouTube");
                    _logger.LogInformation("YOUTUBE REQUEST  URL: \n" + url);

                    var response = await client.GetAsync(url);
                    var responseResults = response.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation("YOUTUBE RESPONSE BODY: \n" + responseResults.ToString());

                    checkCached = JsonConvert.DeserializeObject<YoutubeTrailerResponse>(responseResults);

                    VideoTrailer videoTrailerCache = YouTubeParser(checkCached, movieName);

                    _memoryCache.Set(movieName+"-ytb", checkCached, TimeSpan.FromSeconds(_configuration.GetValue<int>("CacheDuration")));
                }
            }
            return checkCached;
            
        }

        private VideoTrailer YouTubeParser(YoutubeTrailerResponse youTubeTrailer, string searchedMovieName)
        {
            VideoTrailer videoTrailerCache = new VideoTrailer();
            if (youTubeTrailer.items.Count == 1)
            {
                foreach (var item in youTubeTrailer.items)
                {
                    videoTrailerCache.Title = searchedMovieName;
                    videoTrailerCache.Id = item.id.videoId;
                    videoTrailerCache.VIdeoUrl = "https://www.youtube.com/embed/" + item.id.videoId;
                    videoTrailerCache.ImageUrl = item.snippet.thumbnails.medium.url;
                }

                _memoryCache.Set(searchedMovieName + "-ytbCache", videoTrailerCache, TimeSpan.FromSeconds(_configuration.GetValue<int>("CacheDuration")));
                VideoTrailerCacheAbstract.VideoTrailerCaches.Add(searchedMovieName + "-ytbCache");
                VideoTrailerCollection.VideoTrailerCollectionList.Add(videoTrailerCache);
            }
            return videoTrailerCache;
        }
    }
}
