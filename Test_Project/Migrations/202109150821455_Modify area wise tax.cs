namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifyareawisetax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaxAreaWiseMinimumTaxes", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaxAreaWiseMinimumTaxes", "IsActive", c => c.Boolean());
        }
    }
}
