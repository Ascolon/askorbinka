using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Askorbinka.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<Liked> Likeds { get; set; }

        public User()
        {
            Likeds = new List<Liked>();
        }
    }
}