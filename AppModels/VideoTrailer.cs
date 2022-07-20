using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels
{
    public class VideoTrailer
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string VIdeoUrl { get; set; }

        public static implicit operator VideoTrailer(List<VideoTrailer> v)
        {
            throw new NotImplementedException();
        }
    }
}
