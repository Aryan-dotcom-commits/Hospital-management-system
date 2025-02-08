using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Data.ApplicationDB;
using Models.UserModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace backend.Controllers;

[Route("login")]
[ApiController]

public class UserLogin : ControllerBase {
    private readonly ApplicationDB _db;

    public UserLogin(ApplicationDB db) {
        _db = db;
    }

    [HttpPost] 
    public IActionResult Login([FromBody] UserModel userModel) {
        var existingUser = _db.UserModel.FirstOrDefault(u => u.Usermail == userModel.Usermail);
        if(existingUser == null) {
            return Conflict("No such user found");
        }

        _db.UserModel.Add(userModel);
        _db.SaveChanges();

        return Ok(new {message = "Login successful", Usermail = existingUser.Usermail});
    }
}
