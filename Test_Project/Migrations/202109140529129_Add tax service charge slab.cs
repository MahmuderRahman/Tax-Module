namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addservicechargeslab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxServiceChargeSlabs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LimitAbove = c.Decimal(precision: 18, scale: 2),
                        TaxRate = c.Decimal(precision: 18, scale: 2),
                        MinAmount = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(maxLength: 1000),
                        ValidFromHrmFinancialYearId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        HrmFinancialYear_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrmFinancialYears", t => t.HrmFinancialYear_Id)
                .Index(t => t.HrmFinancialYear_Id);
            
            AlterColumn("dbo.TaxInvestmentRebateSlabs", "Description", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxServiceChargeSlabs", "HrmFinancialYear_Id", "dbo.HrmFinancialYears");
            DropIndex("dbo.TaxServiceChargeSlabs", new[] { "HrmFinancialYear_Id" });
            AlterColumn("dbo.TaxInvestmentRebateSlabs", "Description", c => c.String(maxLength: 100));
            DropTable("dbo.TaxServiceChargeSlabs");
        }
    }
}
