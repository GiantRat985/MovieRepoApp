using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoApp.Models
{
    /// <summary>
    /// Holds user provided data about a specific movie.
    /// </summary>
    public class MovieUserData
    {
        public DateTime LastWatchDate {  get; set; }
        public int Rating { get; set; }
        public bool Favorite { get; set; }
        public int WatchCounter { get; set; }
    }
}
