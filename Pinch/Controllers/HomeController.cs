using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinch.Data;
using Pinch.Services.Interfaces;
using Pinch.ViewModels.Home;

namespace Pinch.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IHomeServices _homeService;
        protected readonly IIngredientService _ingredientService;

        public HomeController(IHomeServices homeService, IIngredientService ingredientService)
        {
            _homeService = homeService;
            _ingredientService = ingredientService;
        }

        public ActionResult Index()
        {
            var indexViewModel = new IndexViewModel
            {
                FavouriteRecipes = _homeService.FavouriteRecipes().ToList(),
                Weekdaymeals = _ingredientService.GetFewerIngredientsRecipes().ToList(),
                PopularIngredients = _ingredientService.GetPopularIngredients().ToList()
            };
           
            return View(indexViewModel);
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