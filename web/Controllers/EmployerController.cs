using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly EmployerService _employerService;

        public EmployerController(EmployerService employerService) =>
               _employerService = employerService;

        [Route("getProfile")]

        [HttpGet]
        public async Task<Employer> GetEmployers(string id)
        {

            return await _employerService.Getprofile(id);
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> register([FromBody] Employer employerInfo)
        {
            var result = await _employerService.RegisterEmployer(employerInfo);
            if (result)
            {
                return Ok("Registration successful");
            }
            return BadRequest("registration failed");


        }


        [HttpPut("edit/{id}")]

        public async Task<IActionResult> editUSerInfo(string id, [FromBody] EmployerUpdate employerInfo)
        {
            var result = await _employerService.UpdateEmployer(id, employerInfo);
            if (result)
            {
                return Ok("updated successfully");
            }
            return BadRequest("update failed");

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteByID(string id)

        {
            var result = _employerService.DeleteEmployersync(id);
            if (result)
            {
                return Ok("employer deleted successfully");
            }
            return BadRequest("deleting failed");
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LogIn(LoginModel loginInfo)
        {

            if (string.IsNullOrWhiteSpace(loginInfo.name) || string.IsNullOrWhiteSpace(loginInfo.password))
            {
                return BadRequest("invalid input");
            }
            var employer = await _employerService.EmployerLogin(loginInfo);
            if (employer == null)
            {
                return BadRequest("No account found");

            }
            return Ok();


        }
    }

}