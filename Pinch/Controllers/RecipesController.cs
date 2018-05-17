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
        public ActionResult Index(string searchBy, string Search)
        {

            if (searchBy == "RecipeName")
            {
                var recipesNameSearch = _context.Recipes.Where(r => r.Name.Contains(Search)).ToList();
                return View(recipesNameSearch);
            }
            else if (searchBy == "Ingredient")
            {
                var recipeByIngredientSearch = new List<Recipe>();

                var IngredientSearch = _context.Ingredients.Include(i => i.RecipeIngredients.Select(ri => ri.Recipe))
                                                           .Where(i => i.Name.Contains(Search)).ToList();
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
                var recipes = _context.Recipes.ToList();

                return View(recipes);
            }
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

        [Route("Edit/{id}", Name = "RecipeEdit")]
        public ActionResult Edit(int id)
        {
            var recipe = _context.Recipes.Include(r => r.RecipeIngredients.Select(i => i.Ingredient))
                                         .Include(r => r.Instructions).FirstOrDefault(r => r.Id == id);

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

            var recipeToEdit = _context.Recipes.FirstOrDefault(r => r.Id == model.Id);

            if(recipeToEdit == null)
            {
                return HttpNotFound();
            }

            recipeToEdit.Name = model.Name;
            recipeToEdit.Description = model.Description;
            recipeToEdit.IsFavorite = model.IsFavorite;
            recipeToEdit.ImageLink = model.ImageLink;

            _context.SaveChanges();

            return RedirectToRoute("EditRecipeIngredient", new { recipeId = recipeToEdit.Id });
        }

        [Route("EditRecipeIngredient/{recipeId}", Name = "EditRecipeIngredient")]
        public ActionResult EditRecipeIngredient(int recipeId)
        {
            var recipe = _context.Recipes.Include(r => r.RecipeIngredients.Select(ri => ri.Ingredient))
                                .FirstOrDefault(r => r.Id == recipeId);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
            //var editRecipeViewModel = new EditRecipeIngredientViewModel
            //{
            //    Recipe = recipe,
            //    SelectedIngredientName = "",
            //    Ingredients = _context.Ingredients.ToList()
            ////};

            //return View(editRecipeViewModel);
        }

        [HttpPost]
        [Route("EditRecipeIngredient/{recipeId}", Name = "EditRecipeIngredientAjax")]
        public JsonResult EditRecipeIngredient(EditIngredientViewModel model)
        {
            var existingIngredient = _context.Ingredients.FirstOrDefault(i => i.Name == model.IngredientName);

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
            var recipe = _context.Recipes.Include(r => r.Instructions)
                                         .Include(r => r.RecipeIngredients.Select(ri => ri.Ingredient))
                                         .FirstOrDefault(r => r.Id == recipeId);
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
            var recipeInstructionToAdd = new Instruction
            {
                RecipeId = model.RecipeId,
                SequenceOrder = model.SequenceOrder,
                Name = model.Name
            };

            _context.Instructions.Add(recipeInstructionToAdd);
            _context.SaveChanges();


            return Json(recipeInstructionToAdd);
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

        [HttpPost]
        [Route("DeleteIngredient", Name = "DeleteRecipeIngredient")]
        public ActionResult DeleteIngredient(int ingredientId)
        {
            var ingredientToDelete = _context.RecipeIngredients.FirstOrDefault(ri => ri.Id == ingredientId);
            if (ingredientToDelete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            _context.RecipeIngredients.Remove(ingredientToDelete);
            _context.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}