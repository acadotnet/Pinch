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

namespace Pinch.Controllers
{
    [RoutePrefix("Recipes")]
    public class RecipesController : Controller
    {
        protected readonly PinchContext _context;

        public RecipesController()
        {
            _context = new PinchContext();
        }

        // GET: Recipes  
        [Route("", Name = "AllRecipes")]
        public ActionResult Index()
        {
            var Recipes = _context.Recipes.ToList();

            return View(Recipes);
        }

        [Route("Details/{id}", Name = "RecipeDetails")]
        public ActionResult Details(int id)
        {
            var recipe = _context.Recipes
                .Include(r => r.RecipeIngredients.Select(ri => ri.Ingredient))
                .Include(r => r.Instructions)
                .FirstOrDefault(r => r.Id == id);

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

            var recipeToCreate = new Recipe();

            recipeToCreate.Name = model.Name;
            recipeToCreate.Description = model.Description;
            recipeToCreate.IsFavorite = model.IsFavorite;

            _context.Recipes.Add(recipeToCreate);
            _context.SaveChanges();

            return RedirectToRoute("EditRecipeIngredient", new { recipeId = recipeToCreate.Id});
        }

        [Route("EditRecipeIngredient/{recipeId}", Name = "EditRecipeIngredient")]
        public ActionResult EditRecipeIngredient(int recipeId)
        {
            var recipe = _context.Recipes.Include(r => r.RecipeIngredients.Select(ri => ri.Ingredient))
                                .FirstOrDefault(r => r.Id == recipeId);

            return View(recipe);
        }

        [HttpPost]
        [Route("EditRecipeIngredient/{recipeId}", Name = "EditRecipeIngredientAjax")]
        public JsonResult EditRecipeIngredient(EditIngredientViewModel model)
        {
            var existingIngredient = _context.Ingredients.Where(i => i.Name == model.IngredientName).FirstOrDefault();

            if(existingIngredient == null)
            {
                existingIngredient = new Ingredient
                {
                    Name = model.IngredientName
                };

                _context.Ingredients.Add(existingIngredient);
            }

            var recipeIngredientToAdd = new RecipeIngredient
            {
                RecipeId = model.RecipeId,
                Measurement = model.Measurement,
                UnitOfMeasurement = model.UnitOfMeasurement,
                IngredientId = existingIngredient.Id
            };

            _context.RecipeIngredients.Add(recipeIngredientToAdd);
            _context.SaveChanges();

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
            var recipe = _context.Recipes.Include(r => r.Instructions).FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }


        [HttpPost]
        [Route("Editinstructions/{recipeId}", Name = "EditinstructionsAjax")]
        public JsonResult Editinstructions(Recipe model)
        {

            return Json(model);
        }


        [Route("Delete/{id}", Name = "RecipeDelete")]
        public ActionResult Delete(int id)
        {
            var recipeToDelete = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipeToDelete == null)
            {
                return HttpNotFound();
            }

            _context.Recipes.Remove(recipeToDelete);
            _context.SaveChanges();

            return RedirectToRoute("AllRecipes");
        }

    }
}