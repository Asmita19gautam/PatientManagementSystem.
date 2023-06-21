using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMgmtfinal.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Charge { get; set; }

        [ForeignKey("DepID")]
        public Department? Department { get; set; }

        [Display(Name = "Department")]
        public int? DepID { get; set; }
    }
}
