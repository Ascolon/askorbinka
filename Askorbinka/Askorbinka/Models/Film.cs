using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Askorbinka.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Video { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Film()
        {
            Comments = new List<Comment>();
        }
    }
}