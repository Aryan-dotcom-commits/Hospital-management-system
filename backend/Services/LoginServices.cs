using Data.ApplicationDB;
using Models.UserModel;

public class LoginServices {
    private readonly ApplicationDB db;
    private LoginServices(ApplicationDB _db) {
        _db = db;
    }
    
    public async Task<string> LoginMethod(string email, string password) {
        var users = db.UserModel.FirstOrDefault(u => u.Usermail == email);
        if (users == null) {
            return "User not found, please register";
        }

        return "Login sucessful";

    }
}