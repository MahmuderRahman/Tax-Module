namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodelscolumnlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaxInvestmentRebateSlabs", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.TaxAreas", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.TaxAreas", "Remarks", c => c.String(maxLength: 500));
            AlterColumn("dbo.TaxInvestmentRebateAreas", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.TaxInvestmentRebateAreas", "Remarks", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaxInvestmentRebateAreas", "Remarks", c => c.String());
            AlterColumn("dbo.TaxInvestmentRebateAreas", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.TaxAreas", "Remarks", c => c.String());
            AlterColumn("dbo.TaxAreas", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.TaxInvestmentRebateSlabs", "Description", c => c.String());
        }
    }
}
