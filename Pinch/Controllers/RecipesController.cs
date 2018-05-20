using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pinch.Data;
using Pinch.Models;
using Pinch.ViewModels;
using Pinch.ViewModels.Recipes;
using System.Net;
using Pinch.Services;
using Pinch.Services.Interfaces;

namespace Pinch.Controllers
{
    [RoutePrefix("Recipes")]
    public class RecipesController : Controller
    {
        protected readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: Recipes  
        [Route("", Name = "AllRecipes")]
        public ActionResult Index(string searchBy, string Search)
        {

            if (searchBy == "RecipeName")
            {
                var recipesNameSearch = _recipeService.RecipeSearchByName(Search);
                return View(recipesNameSearch);
            }
            else if (searchBy == "Ingredient")
            {
                var recipeByIngredientSearch = new List<Recipe>();

                var IngredientSearch = _recipeService.RecipeSearchByIngredientName(Search).ToList();
                foreach (var ingredient in IngredientSearch)
                {
                    foreach(var recipeIngredient in ingredient.RecipeIngredients)
                    {
                        recipeByIngredientSearch.Add(recipeIngredient.Recipe);
                    }
                }

                 return View(recipeByIngredientSearch.Distinct().ToList());
            }
            else
            {
                var recipes = _recipeService.GetRecipes();

                return View(recipes);
            }
        }

        [Route("Details/{id}", Name = "RecipeDetails")]
        public ActionResult Details(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }


        [Route("Create", Name = "RecipeCreate")]
        public ActionResult Create()
        {
            var recipe = new Recipe();
               
            return View(recipe);
        }

        [HttpPost]
        [Route("Create", Name = "RecipeCreatePost")]
        public ActionResult Create(Recipe model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var recipeToCreate = _recipeService.CreateRecipe(model.Name, model.Description, model.IsFavorite, model.ImageLink);

            return RedirectToRoute("EditRecipeIngredient", new { recipeId = recipeToCreate.Id});
        }

        [Route("Edit/{id}", Name = "RecipeEdit")]
        public ActionResult Edit(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        [Route("Edit", Name = "RecipeEditPost")]
        public ActionResult Edit(Recipe model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var recipeToEdit = _recipeService.GetRecipeById(model.Id);

            if(recipeToEdit == null)
            {
                return HttpNotFound();
            }

            _recipeService.EditRecipe(recipeToEdit.Id, model.Name, model.Description, model.IsFavorite, model.ImageLink);

            return RedirectToRoute("EditRecipeIngredient", new { recipeId = recipeToEdit.Id });
        }

        [Route("EditRecipeIngredient/{recipeId}", Name = "EditRecipeIngredient")]
        public ActionResult EditRecipeIngredient(int recipeId)
        {
            var recipe = _recipeService.GetRecipeById(recipeId);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        [Route("EditRecipeIngredient/{recipeId}", Name = "EditRecipeIngredientAjax")]
        public JsonResult EditRecipeIngredient(EditIngredientViewModel model)
        {
            var existingIngredient = _recipeService.GetIngredientByName(model.IngredientName);

            if(existingIngredient == null)
            {
                existingIngredient = _recipeService.AddIngredient(model.IngredientName);
            }

            var recipeIngredientToAdd = _recipeService.AddRecipeIngredient(model.RecipeId, model.Measurement, model.UnitOfMeasurement, existingIngredient.Id);

            return Json(new
            {
                RecipeId = recipeIngredientToAdd.RecipeId,
                Measurement = recipeIngredientToAdd.Measurement,
                UnitOfMeasurement = recipeIngredientToAdd.UnitOfMeasurement,
                IngredientId = recipeIngredientToAdd.IngredientId,
                IngredientName = model.IngredientName
            });
        }

        [Route("Editinstructions/{recipeId}", Name = "Editinstructions")]
        public ActionResult Editinstructions(int recipeId)
        {
            var recipe = _recipeService.GetRecipeById(recipeId);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        [Route("Editinstructions/{recipeId}", Name = "EditinstructionsAjax")]
        public JsonResult Editinstructions(Instruction model)
        {
            return Json(_recipeService.CreateRecipeInstructions(model.RecipeId, model.SequenceOrder, model.Name));
        }

        [Route("Delete/{id}", Name = "RecipeDelete")]
        public ActionResult Delete(int id)
        {
            var recipeToDelete = _recipeService.GetRecipeById(id);

            if (recipeToDelete == null)
            {
                return HttpNotFound();
            }

            _recipeService.DeleteRecipeById(id);

            return RedirectToRoute("AllRecipes");
        }

        [HttpPost]
        [Route("DeleteIngredient", Name = "DeleteRecipeIngredient")]
        public ActionResult DeleteIngredient(int ingredientId)
        {
            var ingredientToDelete = _recipeService.GetRecipeIngredientsByIngredientId(ingredientId);

            if (ingredientToDelete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            _recipeService.DeleteRecipeIngredient(ingredientId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}