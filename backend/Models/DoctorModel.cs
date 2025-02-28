using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.UserModel;

namespace Models.DoctorModel
{
    public class DoctorModel
    {
        [Key]
        public int DoctorId {get; set;}
        [Required]
        public string Speciality { get; set; } = string.Empty;
    
        public int Phone { get; set; }

        public DateTime workingHours {get; set;}

        public List<int> Patient { get; set; } = new List<int>(); // List of patient IDs
    }
}
