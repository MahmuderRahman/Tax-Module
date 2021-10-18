namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtaxemployeeinvestment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxEmployeeInvestments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxInvestmentRebateAreaId = c.Int(nullable: false),
                        HrmFinancialYearId = c.Int(nullable: false),
                        HrmEmployeeId = c.Int(nullable: false),
                        TotalInvestment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.HrmFinancialYears", t => t.HrmFinancialYearId, cascadeDelete: true)
                .ForeignKey("dbo.TaxInvestmentRebateAreas", t => t.TaxInvestmentRebateAreaId, cascadeDelete: true)
                .Index(t => t.TaxInvestmentRebateAreaId)
                .Index(t => t.HrmFinancialYearId)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxEmployeeInvestments", "TaxInvestmentRebateAreaId", "dbo.TaxInvestmentRebateAreas");
            DropForeignKey("dbo.TaxEmployeeInvestments", "HrmFinancialYearId", "dbo.HrmFinancialYears");
            DropForeignKey("dbo.TaxEmployeeInvestments", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.TaxEmployeeInvestments", new[] { "Employee_Id" });
            DropIndex("dbo.TaxEmployeeInvestments", new[] { "HrmFinancialYearId" });
            DropIndex("dbo.TaxEmployeeInvestments", new[] { "TaxInvestmentRebateAreaId" });
            DropTable("dbo.TaxEmployeeInvestments");
        }
    }
}
