namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactortaxarea : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TaxAreas", "Name", unique: true, name: "UK_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaxAreas", "UK_Name");
        }
    }
}
