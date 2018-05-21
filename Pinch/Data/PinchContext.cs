using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Pinch.Models;


namespace Pinch.Data
{
    public class PinchContext : DbContext
    {
        public PinchContext()
           : base("Name=PinchContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PinchContext, Migrations.Configuration>());
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Upload> Uploads { get; set; }
    }
}