using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.UserModel;

namespace backend.Models
{
    public class Patient : UserModel
    {
        public List<int> SharedWithDoctors { get; set; } = new List<int>(); // Doctors who can access history

        public string Address { get; set; } = string.Empty;
    }
}
