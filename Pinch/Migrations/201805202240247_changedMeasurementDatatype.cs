namespace Pinch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedMeasurementDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecipeIngredients", "Measurement", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecipeIngredients", "Measurement", c => c.Int(nullable: false));
        }
    }
}
