using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers
{
[ApiController]
[Route("/[controller]")]
public class ServiceProviderController:ControllerBase
{

 private readonly ServiceProviderService _ServiceProviderService;

    public ServiceProviderController(ServiceProviderService ServiceProviderService) =>
        _ServiceProviderService = ServiceProviderService;
        [Route("getUsers")]

    [HttpGet]
    public async Task<List<UserInformation>> GetUsers(){
        
        return await _ServiceProviderService.GetAsync();
    }
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> register([FromBody]UserInformation UserInformation){
         await _ServiceProviderService.CreateAsync(UserInformation);

        return CreatedAtAction(nameof(GetUsers), new { Id = UserInformation.Id }, UserInformation);
    }

     
    [HttpPut("edit/{id}")]

    public async Task<IActionResult> editUSerInfo(string id, [FromBody]string name){
        await _ServiceProviderService.AddToItems(id,name);
        return NoContent();

    }
   
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteByID(string id){
        await _ServiceProviderService.DeleteAsync(id);
        return NoContent();
    }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LogIn(LoginModel loginInfo)
        {
            if (string.IsNullOrWhiteSpace(loginInfo.name) || string.IsNullOrWhiteSpace(loginInfo.password)){
                return BadRequest("invalid input");
            }
          var usr = await _ServiceProviderService.ServiceProviderLogin(loginInfo);
          if(usr== null){
         return BadRequest("No user found");

          }
         return Ok();

        }







    }
}
