using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Data.ApplicationDB;
using Models.UserModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

    // Register user
    [HttpPost("register")]
public IActionResult RegisterUser([FromBody] UserModel user) {
    if (user == null) {
        return BadRequest("User data is required");
    }

    try
    {
        // Check if user already exists
        var existingUser = _db.UserModel.FirstOrDefault(u => u.Usermail == user.Usermail);
        if (existingUser != null) {
            return Conflict("User already exists with the given email.");
        }

        // Validate and append "@gmail.com" if missing
        if (!user.Usermail.Contains(MustHaveMail)) {
            user.Usermail += MustHaveMail;
        }

        // Reject email if it's just "@gmail.com"
        if (user.Usermail == "@gmail.com") {
            return Conflict("Invalid email format.");
        }

        // Add user to the database
        _db.UserModel.Add(user);
        _db.SaveChanges();

        return CreatedAtAction(nameof(GetId), new { id = user.userID }, user);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Internal server error. Please try again later.");
    }
}


    // Delete user
    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] int id) {
        var user = _db.UserModel.FirstOrDefault(u => u.userID == id);
        if (user == null) {
            return NotFound("User not found.");
        }

        _db.UserModel.Remove(user);
        _db.SaveChanges();

        return Ok($"User with ID {id} has been deleted.");
    }

    // Login user and display cookies
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserModel user) {
        try {
            var existingUser = _db.UserModel.FirstOrDefault(u => u.Usermail == user.Usermail);
            if (existingUser == null) {
                return Unauthorized(new { Message = "Invalid email or password" });
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

            // Retrieve and display cookies
            var cookies = HttpContext.Request.Cookies;

            return Ok(new {
                Message = "Login Success",
                Cookies = cookies
            });
        }
        catch (Exception ex) {
            return StatusCode(500, new { Message = "An error occurred during login", Error = ex.Message });
        }
    }
}
