namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtaxemployeeasset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxEmployeeAssets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxAssetTypeId = c.Int(nullable: false),
                        HrmFinancialYearId = c.Int(),
                        HrmEmployeeId = c.Int(nullable: false),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.HrmFinancialYears", t => t.HrmFinancialYearId)
                .ForeignKey("dbo.TaxAssetTypes", t => t.TaxAssetTypeId, cascadeDelete: true)
                .Index(t => t.TaxAssetTypeId)
                .Index(t => t.HrmFinancialYearId)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxEmployeeAssets", "TaxAssetTypeId", "dbo.TaxAssetTypes");
            DropForeignKey("dbo.TaxEmployeeAssets", "HrmFinancialYearId", "dbo.HrmFinancialYears");
            DropForeignKey("dbo.TaxEmployeeAssets", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.TaxEmployeeAssets", new[] { "Employee_Id" });
            DropIndex("dbo.TaxEmployeeAssets", new[] { "HrmFinancialYearId" });
            DropIndex("dbo.TaxEmployeeAssets", new[] { "TaxAssetTypeId" });
            DropTable("dbo.TaxEmployeeAssets");
        }
    }
}
