using System.ComponentModel.DataAnnotations;

namespace PatientMgmtfinal.Models
{
    public class DoctorTiming
    {
        [Key]
        public int DrTimeID { get; set; }
        public Doctor? Doctor { get; set; }
        public int? DoctorID { get; set; }
        public TimeSpan AvailableTime { get; set; }
        public DateTime DateAvailable { get; set; }
    }
}
