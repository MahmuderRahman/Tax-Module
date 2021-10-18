using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("TaxSlabTypes")]
    public class TaxSlabType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        [Index("UK_Name", IsUnique = true)]
        public string Name { get; set; }

        [StringLength(1500)]
        public string Remarks { get; set; }
        public int CmnCompanyId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<TaxSlab> TaxSlabs { get; set; }

        public TaxSlabType()
        {
            TaxSlabs = new List<TaxSlab>();
        }
    }
}