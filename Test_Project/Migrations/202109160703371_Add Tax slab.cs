namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaxslab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxSlabs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxSlabTypeId = c.Int(nullable: false),
                        LimitAbove = c.Decimal(precision: 18, scale: 2),
                        TaxRate = c.Decimal(precision: 18, scale: 2),
                        TaxAmount = c.Decimal(precision: 18, scale: 2),
                        ValidFromHrmFinancialYearId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxSlabs", "HrmFinancialYear_Id", "dbo.HrmFinancialYears");
            DropIndex("dbo.TaxSlabs", new[] { "HrmFinancialYear_Id" });
            DropTable("dbo.TaxSlabs");
        }
    }
}
