using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.UserModel;

namespace Models.Patient
{
    public class Patient 
    {
        [Key]
        public int PatientID {get; set;}
        public List<int> SharedWithDoctors { get; set; } = new List<int>(); // Doctors who can access history

        public string Address { get; set; } = string.Empty;
    }
}
