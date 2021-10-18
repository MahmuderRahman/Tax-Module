using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("TaxEmployeeInvestments")]
    public class TaxEmployeeInvestment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TaxInvestmentRebateAreaId { get; set; }
        public TaxInvestmentRebateArea TaxInvestmentRebateArea { get; set; }
        public int HrmFinancialYearId { get; set; }
        public HrmFinancialYear HrmFinancialYear { get; set; }
        public int HrmEmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal TotalInvestment { get; set; }
        public int CmnCompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}