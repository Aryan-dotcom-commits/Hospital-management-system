using Models.UserModel;
using DTO.LoginDTO;
namespace Mappers.UserLogin {
    public class UserLoginMapper {
    public static UserModel MapToUser(LoginDto login) {
        return new UserModel {
            Usermail = login.Usermail,
            password = login.password,
        };
    }
}
}