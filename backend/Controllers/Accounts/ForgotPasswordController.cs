using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Data.ApplicationDB;
using Models.UserModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers.accounts {
    [Route("account")]
    [ApiController]

    public class ForgotPasswordController : ControllerBase {
        private readonly ApplicationDB _db;

        public ForgotPasswordController(ApplicationDB db) {
            _db = db;
        }

        [HttpGet("{mail}")]
        public IActionResult GetEmail([FromRoute] string mail) {
            var existingMail = _db.UserModel.FirstOrDefault(u => u.Usermail == mail);
            
            if(existingMail == null) {
                return NotFound("Mail not found");
            }

            return Ok(existingMail);
        }
    }
}
