using MasterDetailSample01.Models.DomainModels.IdentityDto;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;


namespace MasterDetailSample01.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        #region [- Private Feileds]
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        #endregion

        #region [- ctor -]
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }
        #endregion

        #region [- Login() -]
        [HttpPost]
        [Route("login")]
        //Check the username and password → get the user's roles → create claims →
        //create a JWT → return the token to the client.
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName), //Add the user's username as a Name claim.
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//It creates a unique identifier for the token.
                    };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();//401 Unauthorized
        }

        #endregion

        #region [- Register() -]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = 500, Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = 500, Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = 200, Message = "User created successfully!" });
        }
        #endregion

        #region [- RegisterAdmin() -]
        [HttpPost]
        [Authorize()]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = 500, Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = 500, Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = 200, Message = "User created successfully!" });
        }
        #endregion

        #region [- GetToken(List<Claim> authClaims) -]
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        #endregion

    }
}