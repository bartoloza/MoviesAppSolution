using AppModels;
using Newtonsoft.Json;

namespace MoviesAPI.Services
{
    public class YoutubeService : IYoutubeService
    {
        public async Task<YoutubeTrailerResponse> GetTrailerByMovieNameAsync(string movieName)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://youtube.googleapis.com/youtube/v3/search?part=snippet&maxResults=1&q={movieName}%20official%20trailer&topicId=%2Fm%2F02vxn&key=AIzaSyCEj7IhNnomHoZ8ZSQC8Bun79R1Xg3CxTo";

                var response = await client.GetAsync(url);
                var responseResults = response.Content.ReadAsStringAsync().Result;

                YoutubeTrailerResponse moviceCollection = JsonConvert.DeserializeObject<YoutubeTrailerResponse>(responseResults);

                return moviceCollection;
            }
        }
    }
}
