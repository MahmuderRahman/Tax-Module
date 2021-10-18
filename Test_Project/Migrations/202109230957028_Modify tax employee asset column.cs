namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifytaxemployeeassetcolumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaxEmployeeAssets", "HrmFinancialYearId", "dbo.HrmFinancialYears");
            DropIndex("dbo.TaxEmployeeAssets", new[] { "HrmFinancialYearId" });
            AlterColumn("dbo.TaxEmployeeAssets", "HrmFinancialYearId", c => c.Int(nullable: false));
            AlterColumn("dbo.TaxEmployeeAssets", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.TaxEmployeeAssets", "HrmFinancialYearId");
            AddForeignKey("dbo.TaxEmployeeAssets", "HrmFinancialYearId", "dbo.HrmFinancialYears", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxEmployeeAssets", "HrmFinancialYearId", "dbo.HrmFinancialYears");
            DropIndex("dbo.TaxEmployeeAssets", new[] { "HrmFinancialYearId" });
            AlterColumn("dbo.TaxEmployeeAssets", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TaxEmployeeAssets", "HrmFinancialYearId", c => c.Int());
            CreateIndex("dbo.TaxEmployeeAssets", "HrmFinancialYearId");
            AddForeignKey("dbo.TaxEmployeeAssets", "HrmFinancialYearId", "dbo.HrmFinancialYears", "Id");
        }
    }
}
