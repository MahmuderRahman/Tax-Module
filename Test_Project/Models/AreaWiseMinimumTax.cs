using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("TaxAreaWiseMinimumTaxes")]
    public class AreaWiseMinimumTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TaxAreaId { get; set; }
        public virtual TaxArea TaxArea { get; set; }
        public decimal? MinimumTax { get; set; }
        public int ValidFromHrmFinancialYearId { get; set; }
        public virtual HrmFinancialYear HrmFinancialYear { get; set; }
        public bool IsActive { get; set; }
        public int CmnCompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}