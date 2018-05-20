using Pinch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Pinch.Models;
using Pinch.ViewModels.Ingredients;
using Pinch.Services.Interfaces;

namespace Pinch.Controllers
{
    [RoutePrefix("Ingredients")]
    public class IngredientsController : Controller
    {
        protected readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        // GET: Ingredients
        [Route("", Name = "AllIngredients")]
        public ActionResult Index()
        {
            var ingredients = _ingredientService.Get();

            return View(ingredients);
        }

        [Route("Details/{ingredientId}", Name = "IngredientDetails")]
        public ActionResult Details(int ingredientId)
        {
            var detailsViewModels = new DetailsViewModel
            {
                Ingredient = _ingredientService.GetIngredientById(ingredientId),
                Recipes = _ingredientService.GetRecipiesByIngredientId(ingredientId).ToList()
            };
                
            return View(detailsViewModels);
        }
    }
}