namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifytaxslabtablecolumnorder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaxSlabs", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaxSlabs", "Order", c => c.Int());
        }
    }
}
