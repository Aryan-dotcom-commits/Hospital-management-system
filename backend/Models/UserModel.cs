using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.UserModel {
    public class UserModel {
        [Key]
        public int userID {get; set;}
        
        public string Username { get; set; } = string.Empty;
        public string Usermail { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int Userage { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public UserType userType {get; set;}
        public BloodGroup BloodGroup { get; set; }
        public Diagnosis Diagnosis { get; set; }
    }
}