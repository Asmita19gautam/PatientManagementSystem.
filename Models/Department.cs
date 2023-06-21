using System.ComponentModel.DataAnnotations;

namespace PatientMgmtfinal.Models
{
    public class Department
    {
        [Key]
        public int DepID { get; set; }
        public string Name { get; set; }
    }
}
