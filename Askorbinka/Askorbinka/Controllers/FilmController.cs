using Askorbinka.AttributeCross;
using Askorbinka.Context;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using Askorbinka.Models;
using System.Threading;
using Newtonsoft.Json;

namespace Askorbinka.Controllers
{
    class ThisLiked
    {
        public bool Appreciated { get; set; } = true;
    }
    [CrossOriginAccess]
    public class FilmController : Controller
    {
        AskorbinkaContext context = new AskorbinkaContext();

        public JsonResult GetAllGenre()
        {
            var genres = context.Films.Select(g => g.Genre).Distinct().ToList();
            //
            return Json(genres, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFilmsByGenre(string s)
        {
            var Films = context.Films.Where(g => g.Genre == s).Take(60).ToList(); 
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFilmsById(int id)
        {
            var Film = context.Films.Include(g => g.Comments).FirstOrDefault(v => v.FilmId == id);
            //
            return Json(Film, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLastAdded()
        {
            var Films = context.Films.Include(g => g.Comments).Take(60).ToList();
            //
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteFilm(int id)
        {
            var f = context.Films.FirstOrDefault(g => g.FilmId == id);
            context.Films.Remove(f);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult UploadContent(int s, string g)
        {
            var Films = context.Films.Where(v => v.Genre == g).OrderBy(v => v.FilmId).Skip(s).Take(60).ToList();
            //
            return Json(Films, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Like(int f, int u)
        {
            Film EvaluatedMovie = null;
            Liked Needed = null;
            EvaluatedMovie = context.Films.FirstOrDefault(g => g.FilmId == f);
            var likedfilms = context
                .Users.Include(g => g.Likeds.Select(l => l.LikedFilm))
                .FirstOrDefault(g => g.UserId == u)
                .Likeds;
            Needed = likedfilms.FirstOrDefault(g => g.LikedFilm == EvaluatedMovie);
            if (Needed != null && Needed.Evaluation == Evaluation.Like)
            {
                return Json(new
                {
                    LikeCount = EvaluatedMovie.Like,
                    DislikeCount = EvaluatedMovie.DisLike
                }, JsonRequestBehavior.AllowGet);
            }
            if (Needed != null && Needed.Evaluation == Evaluation.Dislike)
            {
                EvaluatedMovie.DisLike -= 1;
                EvaluatedMovie.Like += 1;
                Needed.Evaluation = Evaluation.Like;
                context.SaveChanges();
                return Json(new
                {
                    LikeCount = EvaluatedMovie.Like,
                    DislikeCount = EvaluatedMovie.DisLike
                }, JsonRequestBehavior.AllowGet);
            }
            if (Needed == null)
            {
                EvaluatedMovie.Like += 1;
                Needed = new Liked()
                {
                    Evaluation = Evaluation.Like,
                    LikedFilm = EvaluatedMovie
                };
                likedfilms.Add(Needed);
                context.SaveChanges();
                return Json(new
                {
                    LikeCount = EvaluatedMovie.Like,
                    DislikeCount = EvaluatedMovie.DisLike
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                LikeCount = EvaluatedMovie.Like,
                DislikeCount = EvaluatedMovie.DisLike
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisLike(int f, int u)
        {
            Film EvaluatedMovie = null;
            Liked Needed = null;
            EvaluatedMovie = context.Films.FirstOrDefault(g => g.FilmId == f);
            var likedfilms = context
                .Users.Include(g => g.Likeds.Select(l => l.LikedFilm))
                .FirstOrDefault(g => g.UserId == u)
                .Likeds;
            Needed = likedfilms.FirstOrDefault(g => g.LikedFilm == EvaluatedMovie);
            if (Needed != null && Needed.Evaluation == Evaluation.Dislike)
            {
                return Json(new
                {
                    LikeCount = EvaluatedMovie.Like,
                    DislikeCount = EvaluatedMovie.DisLike
                }, JsonRequestBehavior.AllowGet);
            }
            if (Needed != null && Needed.Evaluation == Evaluation.Like)
            {
                EvaluatedMovie.DisLike += 1;
                EvaluatedMovie.Like -= 1;
                Needed.Evaluation = Evaluation.Dislike;
                context.SaveChanges();
                return Json(new
                {
                    LikeCount = EvaluatedMovie.Like,
                    DislikeCount = EvaluatedMovie.DisLike
                }, JsonRequestBehavior.AllowGet);
            }
            if (Needed == null)
            {
                EvaluatedMovie.DisLike += 1;
                Needed = new Liked()
                {
                    Evaluation = Evaluation.Dislike,
                    LikedFilm = EvaluatedMovie
                };
                likedfilms.Add(Needed);
                context.SaveChanges();
                return Json(new
                {
                    LikeCount = EvaluatedMovie.Like,
                    DislikeCount = EvaluatedMovie.DisLike
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Text = "finaly",
                LikeCount = EvaluatedMovie.Like,
                DislikeCount = EvaluatedMovie.DisLike
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCommnetToFilm(int f, string t, int u)
        {
            var film = context.Films.FirstOrDefault(g => g.FilmId == f);
            var user = context.Users.FirstOrDefault(g => g.UserId == u);
            if (user != null && film != null)
            {
                film.Comments.Add(new Comment()
                {
                    CommentTime = DateTime.Now.ToLocalTime().ToString(),
                    Content = t,
                    FilmId = film.FilmId,
                    Author = user.Login,
                    AuthorId = user.UserId
                });
                context.SaveChanges();
            }
            var data = context.Comments.Where(c => c.FilmId == film.FilmId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}