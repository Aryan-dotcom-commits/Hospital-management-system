using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Models.UserModel {
    public class UserModel {
        [Key]
        public int userID {get; set;}
        public string Username { get; set; } = string.Empty;
        public string Usermail { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int Userage { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string userType {get; set;} = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
    }
}