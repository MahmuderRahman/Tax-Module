using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("TaxEmployeeAssets")]
    public class TaxEmployeeAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int TaxAssetTypeId { get; set; }
        public TaxAssetType TaxAssetType { get; set; }
        public int HrmFinancialYearId { get; set; }
        public HrmFinancialYear HrmFinancialYear { get; set; }
        public int HrmEmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public int CmnCompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}