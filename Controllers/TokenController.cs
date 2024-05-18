using EtiqaFreelancerDataAPI.Data;
using EtiqaFreelancerDataAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EtiqaFreelancerDataAPI.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ITokenRepository _token;

        public TokenController(IConfiguration config, ITokenRepository token)
        {
            _config = config;
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }


        [AllowAnonymous]
        [HttpPost]
        //[HttpPost, Route("Login")]
        public IActionResult Post([FromBody] LoginInfo obj)
        {
            if (obj == null)
            {
                return BadRequest("Invalid request");
            }

            if (_token.IsLogin(obj.Username, obj.Userpassword))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                     issuer: _config["Jwt:Issuer"],
                     audience: _config["Jwt:Audience"],
                     claims: new List<Claim>(),
                     expires: DateTime.Now.AddMinutes(30),
                     signingCredentials: signinCredentials
                 );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {

                return Unauthorized();
            }



        }




    }
}
