using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IUserRepository repo, IConfiguration conf)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = repo;
            _config = conf;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ActionResult<string>>> Register(RegisterDTO dto)
        {
            IdentityUser user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
            User u = new User(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Email, new Location());

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
                result = await _userManager.AddToRoleAsync(user, "User");

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    _userRepository.Add(u);
                    _userRepository.SaveChanges();

                    string token = GetToken(user, roles);
                    return Created("", token);
                }
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<String>> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            if (user != null){
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
                var roles = await _userManager.GetRolesAsync(user);
                if (result.Succeeded)
                {
                    string token = GetToken(user, roles);
                    return Created("", token);
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("CheckUsername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        [AllowAnonymous]
        [HttpGet("Roles")]
        public async Task<IEnumerable<string>> GetRollesOfUser(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);
            return roles;
        }

        private string GetToken(IdentityUser user, IList<string> roles)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            });

            if (roles.Count > 0)
            {
                foreach (var role in roles) { claims.AddClaim(new Claim("userRoles", role)); }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(null, null, claims.Claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
