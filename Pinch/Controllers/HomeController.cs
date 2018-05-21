using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinch.Data;
using Pinch.Services.Interfaces;

namespace Pinch.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IHomeServices _homeService;

        public HomeController(IHomeServices homeService)
        {
            _homeService = homeService;
        }

        public ActionResult Index()
        {
            var recipes = _homeService.FavouriteRecipes();

            return View(recipes);
        }

        public ActionResult Gallery()
        {
            var recipes = _homeService.Get();

            return View(recipes);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}