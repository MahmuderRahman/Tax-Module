namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addareawiseminimumtax : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxAreaWiseMinimumTaxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxAreaId = c.Int(nullable: false),
                        MinimumTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValidFromHrmFinancialYearId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        HrmFinancialYear_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrmFinancialYears", t => t.HrmFinancialYear_Id)
                .ForeignKey("dbo.TaxAreas", t => t.TaxAreaId, cascadeDelete: true)
                .Index(t => t.TaxAreaId)
                .Index(t => t.HrmFinancialYear_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxAreaWiseMinimumTaxes", "TaxAreaId", "dbo.TaxAreas");
            DropForeignKey("dbo.TaxAreaWiseMinimumTaxes", "HrmFinancialYear_Id", "dbo.HrmFinancialYears");
            DropIndex("dbo.TaxAreaWiseMinimumTaxes", new[] { "HrmFinancialYear_Id" });
            DropIndex("dbo.TaxAreaWiseMinimumTaxes", new[] { "TaxAreaId" });
            DropTable("dbo.TaxAreaWiseMinimumTaxes");
        }
    }
}
