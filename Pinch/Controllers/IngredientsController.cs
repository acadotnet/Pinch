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
using Pinch.ViewModels.Ingredients;
using Pinch.ViewModels.Recipes;

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

        [Route("FewerIngredientsRecipes", Name = "FewerIngredientsRecipes")]
        public ActionResult FewerIngredientsRecipes()
        {
            var recipes = _ingredientService.GetFewerIngredientsRecipes();

            return View(recipes);
        }

        [Route("IngredientDetails/{recipeId}/{ingredientId}", Name = "IngredientDetailsnew")]
        public ActionResult IngredientDetails(int ingredientId, int recipeId)
        {
            var ingredientDetailsViewModel = new IngredientDetailsNewViewModel
            {
                Ingredient = _ingredientService.GetIngredientById(ingredientId),
                RecipeId = recipeId
            };

            return View(ingredientDetailsViewModel);
        }

        [HttpPost]
        [Route("IngredientDetails/{recipeId}/{ingredientId}", Name = "IngredientDetailsnewPost")]
        public ActionResult IngredientDetails(IngredientDetailsNewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ingredientToCreate = _ingredientService.EditIngredient(model.Ingredient);
                
            return RedirectToRoute("EditRecipeIngredient", new { recipeId = model.RecipeId });
        }



    }
}