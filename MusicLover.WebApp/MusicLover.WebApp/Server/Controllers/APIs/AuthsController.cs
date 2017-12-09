using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MusicLover.WebApp.Server.Core.Models;
using MusicLover.WebApp.Server.Persistent;

namespace MusicLover.WebApp.Server.Controllers.APIs
{
    [Route("api/auth")]
    public class AuthsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly IPasswordHasher<ApplicationUser> _hasher;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationRoot _config;

        public AuthsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> logger, IPasswordHasher<ApplicationUser> hasher,
            UserManager<ApplicationUser> userManager,
            IConfigurationRoot config)
        {
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
            _hasher = hasher;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] Credential credential)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(credential.UserName);
                if (user == null)
                    return NotFound();
                if (_hasher.VerifyHashedPassword(user, user.PasswordHash, credential.Password) ==
                    PasswordVerificationResult.Failed)
                {

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _config["Tokens: Issuer"],
                        audience: _config["Tokens:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(15),
                        signingCredentials: creds
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });


                }
            }

            catch
            {
               

            }
            return BadRequest();
        }
    }
}
