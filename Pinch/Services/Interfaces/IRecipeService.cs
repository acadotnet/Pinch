using Pinch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.Services.Interfaces
{
    public interface IRecipeService
    {
        IEnumerable<Recipe> GetRecipes();

        IEnumerable<Recipe> RecipeSearchByName(string name);

        IEnumerable<Ingredient> RecipeSearchByIngredientName(string IngredientName);

        Recipe GetRecipeById(int id);

        void DeleteRecipeById(int id);

        RecipeIngredient GetRecipeIngredientsByIngredientId(int ingredientId);

        void DeleteRecipeIngredient(int ingredientId);

        Instruction CreateRecipeInstructions(int id, int sequence, string name);

        Recipe CreateRecipe(string name, string desc, bool isfav, string image);

        void EditRecipe(int id, string name, string desc, bool isfav, string image);

        Ingredient GetIngredientByName(string name);

        Ingredient AddIngredient(string name);

        RecipeIngredient AddRecipeIngredient(int id, int measurement, string unit, int ingredientId);
    }
}