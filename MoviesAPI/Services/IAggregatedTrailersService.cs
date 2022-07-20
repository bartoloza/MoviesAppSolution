using AppModels;

namespace MoviesAPI.Services
{
    public interface IAggregatedTrailersService
    {
        public Task<List<VideoTrailer>> GetAggregatedTrailers();

    }
}
