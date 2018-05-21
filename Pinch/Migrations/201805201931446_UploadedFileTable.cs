namespace Pinch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UploadedFileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileDataType = c.String(),
                        FileData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Uploads");
        }
    }
}
