using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("TaxSlabs")]
    public class TaxSlab
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TaxSlabTypeId { get; set; }
        public decimal? LimitAbove { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? TaxAmount { get; set; }
        public int ValidFromHrmFinancialYearId { get; set; }
        public virtual HrmFinancialYear HrmFinancialYear { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public int CmnCompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public TaxSlabType TaxSlabType { get; set; }
    }
}