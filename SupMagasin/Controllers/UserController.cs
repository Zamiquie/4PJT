using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    [Authorize]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration { get; set; }

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
   
        // POST: user/User
        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public IActionResult Post([FromBody] User user)
        {
            if (user.Login == "SupMagasin" && user.Password == "Supinf0!")
            {
                user.Token = CreateToken(user);
                return Ok(user);

            }
            else
            {
                return BadRequest(new { message = "login not resolved. Try Again band of little green hacker. ヽ༼ ಠ益ಠ ༽ﾉ" });
            }

        }

        private string CreateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"])); // on recupere le byte de la clé (dans app.setting)
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // on signe le token

            //on creer le token
            var token = new JwtSecurityToken(_configuration["jwt:issuer"], // --> propriétaire
                                             _configuration["jwt:audiance"],// --> destination 
                                             expires: DateTime.Now.AddHours(24), // --> temps de validité
                                             signingCredentials : credentials); // --> signature avec clé

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
