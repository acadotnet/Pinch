namespace Pinch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialSetUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Measurement = c.Int(nullable: false),
                        UnitOfMeasurement = c.String(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        IsFavorite = c.Boolean(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SequenceOrder = c.Int(nullable: false),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeIngredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Instructions", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "IngredientId", "dbo.Ingredients");
            DropIndex("dbo.Instructions", new[] { "RecipeId" });
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientId" });
            DropIndex("dbo.RecipeIngredients", new[] { "RecipeId" });
            DropTable("dbo.Instructions");
            DropTable("dbo.Recipes");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Ingredients");
        }
    }
}
