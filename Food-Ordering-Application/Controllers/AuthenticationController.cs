using Food_Ordering_Application.Authentication;
using Food_Ordering_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController (UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, string role)
        {
            var UserExist = await _userManager.FindByEmailAsync(registerDto.Email);
            if (UserExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new ResponseMessage { status = "Error", Message = "User already exists" });
            }
            //Adding useer to the database

            User user = new()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username,
                PhoneNumber= registerDto.PhoneNumber

            };

            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseMessage { status = "Error", Message = "User failed to create" });
                }
                //adding role to user
                await _userManager.AddToRoleAsync(user, role);

                return StatusCode(StatusCodes.Status201Created,
                    new ResponseMessage { status = "Success", Message = "User created successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseMessage { status = "Error", Message = "This Role doest not exists" });
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //checking the user exists or not
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            //checking the password
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {

                //claimlist creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                //we add roles to the list
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //generate the token with the claims
                var jwtToken = GetToken(authClaims);

                //returning the token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });

            }

            return Unauthorized();


        }
        //method to generate Jwt token
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

    }
}
