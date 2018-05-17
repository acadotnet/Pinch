using Pinch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Pinch.Models;
using Pinch.ViewModels.Ingredients;

namespace Pinch.Controllers
{
    [RoutePrefix("Ingredients")]
    public class IngredientsController : Controller
    {
        protected readonly PinchContext _context;

        public IngredientsController()
        {
            _context = new PinchContext();
        }

        // GET: Ingredients
        [Route("", Name = "AllIngredients")]
        public ActionResult Index()
        {
            var ingredients = _context.Ingredients.ToList();

            return View(ingredients);
        }

        [Route("Details/{ingredientId}", Name = "IngredientDetails")]
        public ActionResult Details(int ingredientId)
        {
            var detailsViewModels = new DetailsViewModel
            {
                Ingredient = _context.Ingredients.FirstOrDefault(i => i.Id == ingredientId),
                Recipes = _context.RecipeIngredients.Where(ri => ri.IngredientId == ingredientId).Select(ri => ri.Recipe).ToList()
            };

            return View(detailsViewModels);
        }
    }
}