using AppModels;

namespace MoviesAPI.Services
{
    public interface IYoutubeService
    {
        public Task<YoutubeTrailerResponse> GetTrailerByMovieNameAsync(string movieName);
    }
}
