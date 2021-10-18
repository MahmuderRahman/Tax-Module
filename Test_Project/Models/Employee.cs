using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public List<TaxEmployeeAsset> TaxEmployeeAssets { get; set; }
        public List<TaxEmployeeInvestment> TaxEmployeeInvestments { get; set; }
        public Employee()
        {
            TaxEmployeeAssets = new List<TaxEmployeeAsset>();
            TaxEmployeeInvestments = new List<TaxEmployeeInvestment>();
        }

    }
}