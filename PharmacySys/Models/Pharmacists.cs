using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace PharmacySys.Models
{
    public class Pharmacists
    {
        [Key]
        public int PharmacistID { get; set; }
        public int PharmacyID { get; set; }
        public string Name { get; set; }
        public string PrimaryDrug { get; set; }
        public int TotalUnitCountPrimaryDrug { get; set; }
        public int TotalUnitCountOtherDrugs { get; set; }
    }
}
