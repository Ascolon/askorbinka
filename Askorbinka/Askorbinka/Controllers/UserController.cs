using Askorbinka.Context;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Askorbinka.Models;
using System.Threading;

namespace Askorbinka.Controllers
{

    public class UserController : Controller
    {
        AskorbinkaContext context = new AskorbinkaContext();

        class NoUser
        {
            public bool NotFound { get; set; } = true;
        }
        class UserBusy
        {
            public bool Busy { get; set; } = true;
        }
        


        public JsonResult GetUSer(string l, string p)
        {
            Thread.Sleep(10000);
            var user = context.Users.FirstOrDefault(g => g.Login == l && g.Password == p);
            if (user == null)
                return Json(new NoUser(), JsonRequestBehavior.AllowGet);
            user.Likeds = null;
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateUSer(string l, string p)
        {
            if (context.Users.Count(g => g.Login == l) > 0)
                return Json(new UserBusy(), JsonRequestBehavior.AllowGet);
            var user = new Models.User()
            {
                Login = l,
                Password = p
            };
            context.Users.Add(user);
            context.SaveChanges();
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LikedUsers(int id)
        {

            var liked = context
                .Likeds.Include(g => g.LikedFilm)
                .Where(g => g.UserId == id && g.Evaluation == Evaluation.Like)
                .Select(l => l.LikedFilm)
                .ToList();
            return Json(liked, JsonRequestBehavior.AllowGet);
        }
    }
}