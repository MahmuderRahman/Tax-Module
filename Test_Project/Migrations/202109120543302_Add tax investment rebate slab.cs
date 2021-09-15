namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinvestmentrebateslab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HrmFinancialYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaxInvestmentRebateSlabs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LimitAbove = c.Decimal(precision: 18, scale: 2),
                        RebateRate = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
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
            
            CreateTable(
                "dbo.TaxAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Remarks = c.String(),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaxInvestmentRebateAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Remarks = c.String(),
                        Status = c.Boolean(nullable: false),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxInvestmentRebateSlabs", "HrmFinancialYear_Id", "dbo.HrmFinancialYears");
            DropIndex("dbo.TaxInvestmentRebateSlabs", new[] { "HrmFinancialYear_Id" });
            DropTable("dbo.TaxInvestmentRebateAreas");
            DropTable("dbo.TaxAreas");
            DropTable("dbo.TaxInvestmentRebateSlabs");
            DropTable("dbo.HrmFinancialYears");
        }
    }
}
