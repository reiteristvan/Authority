namespace Authority.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixProductAndStyleRelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "Id" });
            CreateIndex("dbo.ProductStyles", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductStyles", new[] { "Id" });
            CreateIndex("dbo.Products", "Id");
        }
    }
}
