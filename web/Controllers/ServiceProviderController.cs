using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ServiceProviderController : ControllerBase
    {

        private readonly ServiceProviderService _ServiceProviderService;

        public ServiceProviderController(ServiceProviderService ServiceProviderService) =>
            _ServiceProviderService = ServiceProviderService;
        [Route("getProfile")]

        [HttpGet]
        public async Task<ActionResult> GetUsers(string id)
        {

            var profile = _ServiceProviderService.GetProfile(id);
            if (profile == null)
            {
                return BadRequest("no information was found");
            }
            return Ok(profile);
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> register([FromBody] UserInformation UserInformation)
        {
            var result = await _ServiceProviderService.CreateAsync(UserInformation);
            if (result)
            {
                return Ok("Registration successfull");
            }
            return BadRequest("Wrong input");


        }


        [HttpPut("edit/{id}")]

        public IActionResult editUSerInfo(string id, [FromBody] UpdateModel updateInfo)
        {

            var result = _ServiceProviderService.UpdateUsers(id, updateInfo);
            if (result)
            {
                return Ok("updated successully");
            }
            return BadRequest("update failed");

        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteByID(string id)
        {
            var deleted = _ServiceProviderService.Delete(id);
            if (deleted)
            {
                return Ok("deleted successfully");

            }
            return NotFound("user not found");

        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LogIn(LoginModel loginInfo)
        {
            if (string.IsNullOrWhiteSpace(loginInfo.name) || string.IsNullOrWhiteSpace(loginInfo.password))
            {
                return BadRequest("invalid input");
            }
            var usr = await _ServiceProviderService.ServiceProviderLogin(loginInfo);
            if (usr == null)
            {
                return BadRequest("No user found");

            }
            return Ok("log in success");

        }







    }
}
