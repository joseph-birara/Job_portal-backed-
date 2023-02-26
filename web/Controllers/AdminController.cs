using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using web.Models;
using web.Services;


namespace web.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AdminService _adminService;

        public AdminController(AdminService AdminSrvice, IConfiguration config)
        {
            _adminService = AdminSrvice;
            _config = config;

        }


        [Route("getAdmins")]


        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAdmins(string id)
        {

            LoginModel result = GetCurrentUser();
            Console.WriteLine(result.name);
            Console.WriteLine(result.password);


            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("no result found");
        }
        [Route("getUsers")]
        [HttpGet]
        public async Task<ActionResult> GetService_providers()
        {

            var result = await _adminService.GetServicProviders();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("no result found");
        }
        [Route("getEmployers")]
        [HttpGet]
        public async Task<ActionResult> Get_employers()
        {

            var result = await _adminService.GetAllEmployers();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("no result found");
        }
        //adds new addmin
        [Route("addAdmin")]
        [HttpPost]
        public async Task<IActionResult> register([FromBody] UserAdmin adminInfo)
        {
            var result = await _adminService.AddAdmin(adminInfo);
            if (result)
            {

                return Ok("admin added succefully");

            }
            return BadRequest("failed");

        }

        //updates admin information 
        [HttpPut("edit/{id}")]

        public async Task<IActionResult> editUSerInfo(string id, [FromBody] AdminUpdate updateInfo)
        {
            var result = await _adminService.UpdateAdmin(id, updateInfo);
            if (result)
            {
                return Ok("updated successfully");
            }
            return BadRequest("failed");

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteByID(string id)
        {
            var result = await _adminService.DeleteAdminAsync(id);
            if (result)
            {
                return Ok("admin deleted successfully");
            }
            return BadRequest("failed");
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LogIn(LoginModel loginInfo)
        {
            if (string.IsNullOrWhiteSpace(loginInfo.name) || string.IsNullOrWhiteSpace(loginInfo.password))
            {
                return BadRequest("invalid input");
            }
            var admin = await _adminService.AdminLogIN(loginInfo);
            if (admin == null)
            {
                return BadRequest("No account found");

            }
            var token = GenerateToken(admin);
            return Ok(token);




        }
        // To generate token
        private string GenerateToken(UserAdmin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.name),
                new Claim(ClaimTypes.Role,"admin")
            };
            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private LoginModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new LoginModel
                {
                    name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    password = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }


    }

}