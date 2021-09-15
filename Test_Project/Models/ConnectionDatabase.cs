using System.Data.Entity;

namespace Test_Project.Models
{
    public class ConnectionDatabase : DbContext
    {
        public ConnectionDatabase() : base("ConnectionDatabase")
        {

        }

        public DbSet<TaxArea> DbSetTaxAreas { get; set; }
        public DbSet<TaxInvestmentRebateArea> DbSetTaxInvestmentRebateAreas { get; set; }
        public DbSet<TaxInvestmentRebateSlab> DbSetTaxInvestmentRebateSlabs { get; set; }
        public DbSet<HrmFinancialYear> DbSetHrmFinancialYears { get; set; }
        public DbSet<TaxAssetType> DbSetTaxAssetTypes { get; set; }
        public DbSet<TaxServiceChargeSlab> DbSetTaxServiceChargeSlabs { get; set; }

    }
}