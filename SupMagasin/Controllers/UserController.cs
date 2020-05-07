using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SupMagasin.Dal;
using SupMagasin.Model;
using SupMagasin.Utils;

namespace SupMagasin.Controllers
{
    
    [Route("user")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors(PolicyName = "PolicyFrontEnd")]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration { get; set; }
        public DalCustomer _dalCustomer { get; set; }
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dalCustomer = new DalCustomer();

        }
   
        // POST: user/auth
        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user.RealyUser)
            {
                var userToLogin = await _dalCustomer.GetCustomerByEmail(user.Login);
                var t = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
                //si le user n'existe pas
                if (userToLogin == null)
                {
                    new WriteLog(TypeLog.AuthenError).WriteFile(user.Login + " unknow in datas (⊙.☉)7 ");
                }
                //si le ùot de passe est eronnée
                else if(userToLogin.Password != t)
                {
                    new WriteLog(TypeLog.AuthenError).WriteFile(userToLogin.Email+ " reason : bad password  (╬ ಠ益ಠ)");
                }
                else
                {
                    user.Token = CreateToken();
                    user.Password = "";
                    user.ID = userToLogin.Id;
                    new WriteLog(TypeLog.AuthenSuccess).WriteFile(userToLogin.Email + " is connected  ヽ(´▽`)/");
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
        //Route : user/create
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody]Customer createUser)
        { 
            // si un element obligatoire manque à la création de compte
            if (String.IsNullOrEmpty(createUser.Email) || String.IsNullOrEmpty(createUser.Password)|| String.IsNullOrEmpty(createUser.Name)|| String.IsNullOrEmpty(createUser.FirstName)|| String.IsNullOrEmpty(createUser.Postal_Code)|| String.IsNullOrEmpty(createUser.City) || String.IsNullOrEmpty(createUser.Adress))
            {
                return BadRequest(new { message = ErrorMissingField(createUser)});
            }

            var nUser = _dalCustomer.GetCustomerByEmail(createUser.Email).Result;
            //on recherche si le user existe dèja
            if(nUser != null)
            {
                return StatusCode(550,"User exist Already");
            }
            //sinon on le creer
            else
            {
                await _dalCustomer.AddCustomerAsync(createUser);
                //on le recherche
                var newUser = await _dalCustomer.GetCustomerByEmail(createUser.Email);
                //on creer un object user
                User logNewUser = new User()
                {
                    ID = newUser.Id,
                    Login = newUser.Email,
                    RealyUser = true,
                    Token = CreateToken()
                };
                //on le renvois avec les nouvelles informations
                return Ok(logNewUser);
            }


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


        //function pour identifier les champs manquants
        private string ErrorMissingField(Customer customer)
        {
            string returnMessage = "Filds missing : {";
            if (String.IsNullOrEmpty(customer.Email )) returnMessage = returnMessage + "Email ";
            if(String.IsNullOrEmpty(customer.Password )) returnMessage = returnMessage + "Password ";
            if (String.IsNullOrEmpty(customer.Name )) returnMessage = returnMessage + "Name ";
            if (String.IsNullOrEmpty(customer.FirstName )) returnMessage = returnMessage + "FirstName ";
            if (String.IsNullOrEmpty(customer.Postal_Code )) returnMessage = returnMessage + "PostalCode ";
            if (String.IsNullOrEmpty(customer.City )) returnMessage = returnMessage + "City ";
            if (String.IsNullOrEmpty(customer.Adress )) returnMessage = returnMessage + "Adress ";
            return returnMessage + "}";

        }
    }
}
