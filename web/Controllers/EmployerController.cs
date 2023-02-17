using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers
{
[ApiController]
[Route("/[controller]")]
public class EmployerController:ControllerBase
{
 private readonly EmployerService _employerService;

 public EmployerController(EmployerService employerService) =>
        _employerService = employerService;

[Route("getProfile")]

    [HttpGet]
    public async Task<Employer> GetEmployers(string id){
        
        return await _employerService.Getprofile(id);
    }
    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> register([FromBody]Employer employerInfo){
         await _employerService.RegisterEmployer(employerInfo);

        return CreatedAtAction(nameof( GetEmployers), new { Id = employerInfo.Id },employerInfo);
    }

     
    [HttpPut("edit/{id}")]

    public async Task<IActionResult> editUSerInfo(string id, [FromBody]string name){
        await _employerService.UpdateEmployer(id,name);
        return NoContent();

    }
   
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteByID(string id){
        await _employerService.DeleteEmployersync(id);
        return NoContent();
    }
        [Route("login")]
        [HttpPost]
        public async Task<Employer> LogIn(LoginModel loginInfo)
        {
          return await _employerService.EmployerLogin(loginInfo);
         

        }
}

}