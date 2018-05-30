namespace Pinch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsMainIngredient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredients", "IsMainIngredient", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredients", "IsMainIngredient");
        }
    }
}
