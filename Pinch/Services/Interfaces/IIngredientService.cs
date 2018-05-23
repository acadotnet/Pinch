using Pinch.Models;
using Pinch.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.Services.Interfaces
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> Get();

        Ingredient GetIngredientById(int ingredientId);

        IEnumerable<Recipe> GetRecipiesByIngredientId(int ingredientId);

        IEnumerable<Recipe> GetFewerIngredientsRecipes();

        IEnumerable<Ingredient> GetPopularIngredients();

    }
}