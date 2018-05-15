using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinch.Data;

namespace Pinch.Controllers
{
    public class HomeController : Controller
    {
        protected readonly PinchContext _context;

        public HomeController()
        {
            _context = new PinchContext();
        }

        public ActionResult Index()
        {
            var recipes = _context.Recipes.Where(r => r.IsFavorite == true).Take(5).ToList();

            return View(recipes);
        }

        public ActionResult Gallery()
        {
            var recipes = _context.Recipes.ToList();

            return View(recipes);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}