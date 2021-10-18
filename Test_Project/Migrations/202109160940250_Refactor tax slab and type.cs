namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactortaxslabandtype : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TaxSlabs", "TaxSlabTypeId");
            AddForeignKey("dbo.TaxSlabs", "TaxSlabTypeId", "dbo.TaxSlabTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxSlabs", "TaxSlabTypeId", "dbo.TaxSlabTypes");
            DropIndex("dbo.TaxSlabs", new[] { "TaxSlabTypeId" });
        }
    }
}
