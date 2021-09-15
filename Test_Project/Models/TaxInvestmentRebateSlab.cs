using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("TaxInvestmentRebateSlabs")]
    public class TaxInvestmentRebateSlab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal? LimitAbove { get; set; }
        public decimal? RebateRate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public int ValidFromHrmFinancialYearId { get; set; }
        public virtual HrmFinancialYear HrmFinancialYear { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}