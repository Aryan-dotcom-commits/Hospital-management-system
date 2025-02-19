using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.UserModel;

namespace backend.Models
{
    public class DoctorModel : UserModel
    {
        [Key]
        public int DoctorID {get; set;}
        [Required]
        public string Specialty { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<int> Patient { get; set; } = new List<int>(); // List of patient IDs
    }
}
