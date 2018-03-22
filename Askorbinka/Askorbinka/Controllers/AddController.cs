using Askorbinka.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Askorbinka.Controllers
{
    public class AddController : Controller
    {
        Askorbinka.Context.AskorbinkaContext context = new Context.AskorbinkaContext();
        // GET: Add
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var a = context.Films.Select(g => g.Genre).Distinct();
            //foreach (var item in a)
            //{
            //    items.Add(new SelectListItem() { Text = item, Value = item });
            //}
            items.Add(new SelectListItem() { Text = "Комедия", Value ="Комедия" });
            items.Add(new SelectListItem() { Text = "Фантастика", Value = "Фантастика" });
            items.Add(new SelectListItem() { Text = "Ужасы", Value = "Ужасы" });
            items.Add(new SelectListItem() { Text = "Триллер", Value = "Триллер" });
            items.Add(new SelectListItem() { Text = "Сказки", Value = "Сказки" });
            items.Add(new SelectListItem() { Text = "Треш", Value = "Треш" });
            items.Add(new SelectListItem() { Text = "Катастрофа", Value = "Катастрофа" });
            items.Add(new SelectListItem() { Text = "Драма", Value = "Драма" });
            items.Add(new SelectListItem() { Text = "Военный", Value = "Военный" });
            items.Add(new SelectListItem() { Text = "Историчесий", Value = "Историчесий" });
            items.Add(new SelectListItem() { Text = "Научный", Value = "Научный" });
            items.Add(new SelectListItem() { Text = "Мюзикл", Value = "Мюзикл" });
            items.Add(new SelectListItem() { Text = "Фэнтази", Value = "Фэнтази" });
            items.Add(new SelectListItem() { Text = "Мистика", Value = "Мистика" });
            items.Add(new SelectListItem() { Text = "Приключения", Value = "Приключения" });
            items.Add(new SelectListItem() { Text = "Биографический", Value = "Биографический" });



            ViewBag.List = items;
            return View();
        }
        public ActionResult AddFilm(string name, int year, string genre, string desc, HttpPostedFileBase poster, HttpPostedFileBase video)
        {
            var M = new Film();
            M.Name = name;
            M.Year = year;
            M.Genre = genre;
            M.Description = desc;
            M.Poster = $"http://localhost:51265/Files/Posters/{poster.FileName}";
            M.Video = $"http://localhost:51265/Files/Videos/{video.FileName}";
            context.Films.Add(M);
            context.SaveChanges();
            video.SaveAs(Server.MapPath("~/Files/Videos/" + video.FileName));
            poster.SaveAs(Server.MapPath("~/Files/Posters/" + poster.FileName));
            return View();
        }
    }
}