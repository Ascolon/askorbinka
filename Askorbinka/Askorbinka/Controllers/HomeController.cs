using Askorbinka.AttributeCross;
using Askorbinka.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace Askorbinka.Controllers
{
    [CrossOriginAccess]
    public class HomeController : Controller
    {
        AskorbinkaContext context = new AskorbinkaContext();

        public ActionResult Index()
        {
            if (context.Films.Count() == 0)
            {
                for (int i = 0; i < 33; i++)
                {
                    context.Films.Add(new Models.Film()
                    {
                        Poster = $"http://localhost:51265/Files/Posters/{i + 1}.jpg",
                        Like = 99,
                        Year = 2018,
                        DisLike = 99
                    });
                }
                context.SaveChanges();
            }
            ViewBag.Data = context.Films.ToList();
            return View();
        }
    }
}