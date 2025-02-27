using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Data.ApplicationDB;
using Models.UserModel;
using DTO.LoginDTO;
using Mappers.UserLogin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers;

[Route("accounts")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class UserRegisterController : ControllerBase {
    private readonly ApplicationDB _db;
    private const string MustHaveMail = "@gmail.com";

    public UserRegisterController(ApplicationDB db) {
        _db = db;
    }

    // Get all users
    [HttpGet("/register")]
    public IActionResult GetAll() {
        var users = _db.UserModel.ToList();
        return Ok(users);
    }

    // Get user by ID
    [HttpGet("{id}")]
    public IActionResult GetId([FromRoute] int id) {
        var user = _db.UserModel.FirstOrDefault(u => u.userID == id);
        if (user == null) return NotFound("User not found");
        return Ok(user);
    }

    [HttpPost("register")]
public IActionResult RegisterUser([FromBody] UserModel user) {
    if (user == null) {
        return BadRequest("User data is required");
    }

    try {
        // Check if user already exists with the provided email (even without @gmail.com)
        var existingUser = _db.UserModel.FirstOrDefault(u => u.Usermail == user.Usermail);
        if (existingUser != null) {
            return Conflict("Mail already in use. Please Login");
        }

        // Validate and append "@gmail.com" if missing
        if (!user.Usermail.Contains(MustHaveMail)) {
            var modifiedEmail = user.Usermail + MustHaveMail; // Append only if @gmail.com is missing

            // Check again if the modified email already exists
            existingUser = _db.UserModel.FirstOrDefault(u => u.Usermail == modifiedEmail);
            if (existingUser != null) {
                return Conflict("Mail already in use. Please Login");
            }

            // Now assign the modified email to the user
            user.Usermail = modifiedEmail;
        }

        // Reject email if it's just "@gmail.com" (invalid format)
        if (user.Usermail == "@gmail.com") {
            return Conflict("Invalid email format.");
        }

        // Add user to the database
        _db.UserModel.Add(user);
        _db.SaveChanges();

        return Ok(new { Message = "Success" }); // Returns success message after registration
    } catch (Exception ex) {
        return StatusCode(500, "Internal server error. Please try again later.");
    }
}

        [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto login) {
        try {
            var existingUser = _db.UserModel.FirstOrDefault(u => u.Usermail == login.Usermail);
            if (existingUser == null) {
                return NotFound(new { Message = "Email not found. Please register." });
            }

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.Username),
                new Claim(ClaimTypes.Email, existingUser.Usermail)
            };

            // Create identity and principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign in the user and store the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Set authentication cookie (valid for 1 minute)
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                HttpOnly = true
            };
            Response.Cookies.Append("AuthToken", "user_authenticated", cookieOptions);

            // Return a success response
            return Ok(new { Message = "Success", cookie = cookieOptions });
        } catch (Exception ex) {
            return StatusCode(500, new { Message = "An error occurred during login", Error = ex.Message });
        }
    }

    // Middleware to check session
    [HttpGet("check-session")]
    public IActionResult CheckSession() {
        if (!Request.Cookies.ContainsKey("AuthToken")) {
            return Redirect("/login");
        }
        return Redirect("/");
    }
}
