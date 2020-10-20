using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService.Data;
using AuthorizationService.Models;
using AuthorizationService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VehicleService.Repository;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTokenController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthTokenController));

        public IConfiguration configuration;
        public readonly IAuthRepository<User> repository; 

        public AuthTokenController(IConfiguration _configuration, IAuthRepository<User> _repository)
        {
            configuration = _configuration;
            repository = _repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserData _userData)
        {
            _log4net.Info("Authentication initiated");

            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await repository.Get(_userData.Email, _userData.Password);
                
                if (user != null)
                {
                    _log4net.Info("login data is correct");

                    _log4net.Info("Token generation initiated");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenkey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email)
                        }),
                        Issuer = configuration["Jwt:Issuer"],
                        Audience = configuration["Jwt:Audience"],
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),
                                                                    SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokencreate = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(tokencreate);

                    _log4net.Info("Token Generated Successfuly");

                    _userData.Id = user.Id;
                    _userData.Name = user.Name;
                    _userData.Token = token;

                    return Ok(_userData);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
