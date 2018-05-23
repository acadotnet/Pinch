namespace Pinch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIngredientImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredients", "IngredientImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredients", "IngredientImage");
        }
    }
}
