    using System.ComponentModel.DataAnnotations;
    using Models.Enums;
    namespace Models.UserModel;


    public class UserModel {
        [Key]
        public int userID {get; set;}
        public string Username {get; set;} = string.Empty;
        [Required]
        public string Usermail {get; set;} = string.Empty;
        public UserType userType {get; set;}
        public string password {get; set;} = string.Empty;
    }