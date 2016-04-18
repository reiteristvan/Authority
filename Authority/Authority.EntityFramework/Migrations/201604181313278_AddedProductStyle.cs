namespace Authority.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductStyle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductStyles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Logo = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Products", "Id");
            AddForeignKey("dbo.Products", "Id", "dbo.ProductStyles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Id", "dbo.ProductStyles");
            DropIndex("dbo.Products", new[] { "Id" });
            DropTable("dbo.ProductStyles");
        }
    }
}
