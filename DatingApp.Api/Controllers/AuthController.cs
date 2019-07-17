using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _Auth;
        public AuthController(IAuthRepository Auth)
        {
            _Auth = Auth;

        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {

            if (await _Auth.UserExist(registerUserDTO.Username.ToLower()))
                return NotFound("User Already Exist");

            var user = new User
            {
                name = registerUserDTO.Username.ToLower()
            };
            await _Auth.Register(user, registerUserDTO.Password);
            return StatusCode(201);


        }
        [AllowAnonymous]
[HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO LoginUserDTO)
        {
            var Loginuser = await _Auth.Login(LoginUserDTO.Username, LoginUserDTO.Password);
            if (Loginuser == null)
            {
                return Unauthorized();
            }
            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier,Loginuser.Id.ToString()),
                    new Claim(ClaimTypes.Name,Loginuser.name)
           };
           
          var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));

          var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

         var tokenDescriptor=new SecurityTokenDescriptor{

             Subject=new ClaimsIdentity(claims),
             Expires=DateTime.Now.AddDays(1),
             SigningCredentials=cred
         }; 

         var tokenhandeler=new JwtSecurityTokenHandler();
         var token=tokenhandeler.CreateToken(tokenDescriptor);

       return Ok(new {
           token=tokenhandeler.WriteToken(token)
       });
        }

    }
}