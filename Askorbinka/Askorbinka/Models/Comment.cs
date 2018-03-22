using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Askorbinka.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string CommentTime { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int FilmId { get; set; }      
    }
}