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
        public string Specialty { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<int> Patient { get; set; } = new List<int>(); // List of patient IDs
    }
}
