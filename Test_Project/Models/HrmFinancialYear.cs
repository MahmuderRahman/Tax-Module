using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    public class HrmFinancialYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaxInvestmentRebateSlab> TaxinvestmentRebateSlab { get; set; }
        public ICollection<TaxServiceChargeSlab> TaxserviceChargeSlab { get; set; }


    }
}