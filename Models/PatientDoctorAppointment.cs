using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMgmtfinal.Models
{
    public class PatientDoctorAppointment
    {
        [Key]
        public int PaDrApID { get; set; }
        public Patient? Patient { get; set; }
        public int? PatientID { get; set; }
        public Doctor? Doctor { get; set; }
        public int? DoctorID { get; set; }

        [ForeignKey("DrTimeID")]
        public DoctorTiming? DoctorTiming { get; set; }

        [Display(Name = "DoctorTiming")]
        public int? DrTimeID { get; set; }

        [ForeignKey("DepID")]
        public Department? Department { get; set; }

        [Display(Name = "Department")]
        public int? DepID { get; set; }
        public DateTime ApppointmentTime { get; set; }
        //public TimeSpan AvailableTime { get; set; }
        //public string ScheduleDays { get; set; }

    }
}
