using Pinch.Data;
using Pinch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.Services.Interfaces
{
    public class EfHomeServices : IHomeServices
    {
        private readonly PinchContext _context;

        public EfHomeServices(PinchContext context)
        {
            _context = context;
        }

        public IEnumerable<Recipe> Get()
        {
            return _context.Recipes.ToList();
        }

        public IEnumerable<Recipe> FavouriteRecipes()
        {
            return _context.Recipes.Where(r => r.IsFavorite == true).Take(5).ToList();
        }
    }
}