using AppModels;
using Newtonsoft.Json;

namespace MoviesAPI.Services
{
    public class IMDBService : IIMDBService
    {
        public async Task<MoviceCollection> GetMoviesByNameAsync(string movieName)
        {
            using (var client = new HttpClient()) 
            {
                var url = $"https://imdb-api.com/en/API/SearchMovie/k_37fmbpj9/{movieName}";

                var response = await client.GetAsync(url);
                var responseResults = response.Content.ReadAsStringAsync().Result;

                MoviceCollection moviceCollection = JsonConvert.DeserializeObject<MoviceCollection>(responseResults);

                return moviceCollection;
            }
        }

        public async Task<TrailerData> GetTrailerByMovieIdAsync(string movieId)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://imdb-api.com/en/API/Trailer/k_37fmbpj9/{movieId}";

                var response = await client.GetAsync(url);
                var responseResults = response.Content.ReadAsStringAsync().Result;

                TrailerData moviceCollection = JsonConvert.DeserializeObject<TrailerData>(responseResults);

                return moviceCollection;
            }
        }

        public async Task<YouTubeTrailerData> GetYoutubeLinkdAsync(string movieId)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://imdb-api.com/en/API/YouTubeTrailer/k_37fmbpj9/{movieId}";

                var response = await client.GetAsync(url);
                var responseResults = response.Content.ReadAsStringAsync().Result;

                YouTubeTrailerData moviceCollection = JsonConvert.DeserializeObject<YouTubeTrailerData>(responseResults);

                return moviceCollection;
            }
        }
    }
}
