namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifyareawiseminimumtax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaxAreaWiseMinimumTaxes", "MinimumTax", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TaxAreaWiseMinimumTaxes", "IsActive", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaxAreaWiseMinimumTaxes", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TaxAreaWiseMinimumTaxes", "MinimumTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
