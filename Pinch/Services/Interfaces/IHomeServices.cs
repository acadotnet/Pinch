using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pinch.Models;

namespace Pinch.Services.Interfaces
{
    public interface IHomeServices
    {
        IEnumerable<Recipe> Get();

        IEnumerable<Recipe> FavouriteRecipes();
    }
}