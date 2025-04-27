using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MovieRepoApp.Models
{
    [Table("Movies")]
    public class MovieEntity
    {
        [PrimaryKey]
        public string ImdbID { get; set; }
        public bool OnWishlist { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Rated { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public string ImdbRating { get; set; }
        public string Type { get; set; }
    }
}
