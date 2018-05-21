namespace Pinch.Migrations
{
    using Pinch.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pinch.Data.PinchContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pinch.Data.PinchContext context)
        {
            return;
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //var recipe = new Recipe
            //{
            //    Name = "Mango Ice Cream",
            //    Description = "Creamy Mango-ish delite",
            //    IsFavorite = true,
            //    ImageLink = "https://3.imimg.com/data3/CF/DA/MY-1721780/fresh-mango-ice-cream-500x500.jpg",
            //    RecipeIngredients = new List<RecipeIngredient>
            //    {
            //        new RecipeIngredient
            //        {
            //            Measurement = "2",
            //            UnitOfMeasurement = "Cups",
            //            Ingredient = new Ingredient
            //            {
            //                Name = "Mango"
            //            }
            //        },
            //         new RecipeIngredient
            //        {
            //            Measurement = "2",
            //            UnitOfMeasurement = "Cups",
            //            Ingredient = new Ingredient
            //            {
            //                Name = "Heavy Cream"
            //            }
            //        },
            //          new RecipeIngredient
            //        {
            //            Measurement = "1",
            //            UnitOfMeasurement = "Pinch",
            //            Ingredient = new Ingredient
            //            {
            //                Name = "Salt"
            //            }
            //        },
            //           new RecipeIngredient
            //        {
            //            Measurement = "2",
            //            UnitOfMeasurement = "Cups",
            //            Ingredient = new Ingredient
            //            {
            //                Name = "Sugar"
            //            }
            //        }
            //    },
            //    Instructions = new List<Instruction>
            //    {
            //        new Instruction
            //        {
            //            SequenceOrder = 1,
            //            Name = "Puree the Mango"
            //        },
            //        new Instruction
            //        {
            //            SequenceOrder = 2,
            //            Name = "Add sal, heavy cream, sugar to the mango puree"
            //        },
            //        new Instruction
            //        {
            //            SequenceOrder = 3,
            //            Name = "Pour in the tray and refrigerate for 8 hours"
            //        }
            //    }
            //};

            //context.Recipes.AddOrUpdate(recipe);

        }
    }
}
