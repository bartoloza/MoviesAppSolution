using AppModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace MoviesAPI.Services
{
    public class AggregatedTrailerservice : IAggregatedTrailersService
    {
        private readonly IMemoryCache _memoryCache;


        public AggregatedTrailerservice(IMemoryCache memoryCache)
        { 
            _memoryCache = memoryCache;
        }

        public async Task<List<VideoTrailer>> GetAggregatedTrailers()
        {
            var trailers = new List<VideoTrailer>();
            VideoTrailer checkCached;

            foreach (var item in VideoTrailerCollection.VideoTrailerCollectionList)
            {
                checkCached = _memoryCache.Get<VideoTrailer>(item);
                if (checkCached != null)
                {
                    trailers.Add(checkCached);
                }
                else
                {
                    trailers.Add(item);
                }
            }

            return trailers;

        }
    }
}
