using System.ComponentModel.DataAnnotations;

namespace PatientMgmtfinal.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AlternateContactNumber { get; set; }

    }
}
