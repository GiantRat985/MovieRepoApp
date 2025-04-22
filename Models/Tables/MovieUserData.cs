using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MovieRepoApp.Models.Tables
{
    /// <summary>
    /// Holds user provided data about a specific movie.
    /// </summary>
    public class MovieUserData
    {
        [PrimaryKey]
        public string Id { get; set; }
        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(MovieEntity))]
        public string ImdbId { get; set; }
        public string FirstWatchDate { get; set; }
        public string LastWatchDate {  get; set; }
        public int Rating { get; set; }
        public bool Favorite { get; set; }
        public int WatchCounter { get; set; }
    }
}
