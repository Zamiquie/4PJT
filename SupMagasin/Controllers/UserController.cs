using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SupMagasin.Dal;
using SupMagasin.Model;

namespace SupMagasin.Controllers
{
    
    [Route("user")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration { get; set; }
        public DalCustomer _dalCustomer { get; set; }
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dalCustomer = new DalCustomer();

        }
   
        // POST: user/User
        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public IActionResult Post([FromBody] User user)
        {
            if (user.RealyUser)
            {
                var userToLogin = _dalCustomer.GetCustomerByEmail(user.Login).Result;
                var t = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
                if (userToLogin != null & userToLogin.Password == t)
                {
                    user.Token = CreateToken();
                    user.ID = userToLogin.Id;
                    return Ok(user);
                }

            }
            else if (user.Login == "SupMagasin" && user.Password == "Supinf0!")
            {
                user.Token = CreateToken();
                user.Password = "***********";
                user.ID = "NoID";
                    return Ok(user);

            }
            // dans tous les cas on retourne non login
            return BadRequest(new { message = "login not resolved. Try Again band of little green hacker. ヽ༼ ಠ益ಠ ༽ﾉ" });
        }

        private string CreateToken()
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
