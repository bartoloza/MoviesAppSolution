using AppModels;

namespace MoviesAPI.Services
{
    public interface IIMDBService
    {
        public Task<MoviceCollection> GetMoviesByNameAsync(string movieName);
        public Task<TrailerData> GetTrailerByMovieIdAsync(string movieId);
    }
}
