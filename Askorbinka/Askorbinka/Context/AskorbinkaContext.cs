using Askorbinka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Askorbinka.Context
{
    public class AskorbinkaContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likeds { get; set; }
        public DbSet<User> Users { get; set; }
    }
}