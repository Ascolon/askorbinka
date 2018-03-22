using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Askorbinka.Models
{
    public enum Evaluation
    {
        Like,
        Dislike
    }
    public class Liked
    {
        public int LikedId { get; set; }
        public int UserId { get; set; }
        public Film LikedFilm { get; set; }
        public Evaluation Evaluation { get; set; }
    }
}