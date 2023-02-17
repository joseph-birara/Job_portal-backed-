using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Services;

namespace web.Controllers
{
[ApiController]
[Route("/[controller]")]
public class AdminController:ControllerBase
{
 private readonly AdminService _adminService;

 public AdminController(AdminService AdminSrvice) =>
        _adminService = AdminSrvice;

[Route("getAdmins")]


    [HttpGet]
    public async Task<List<UserAdmin>> GetAdmins(string id){
        
        return await _adminService.GetAllAdmins(id);
    }
    [Route("getUsers")]
    [HttpGet]
    public async Task<List<UserInformation>> GetService_providers(){
        
        return await _adminService.GetServicProviders();
    }
    [Route("getEmployers")]
    [HttpGet]
    public async Task<List<Employer>> Get_employers(){
        
        return await _adminService.GetAllEmployers();
    }
    //adds new addmin
    [Route("addAdmin")]
    [HttpPost]
    public async Task<IActionResult> register([FromBody]UserAdmin adminInfo){
         await _adminService.AddAdmin(adminInfo);

        return Ok("admin added succefully");
    }

     //updates admin information 
    [HttpPut("edit/{id}")]

    public async Task<IActionResult> editUSerInfo(string id, [FromBody]string name){
        await _adminService.UpdateAdmin(id,name);
        return NoContent();

    }
   
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteByID(string id){
        await _adminService.DeleteAdminAsync(id);
        return NoContent();
    }
        [Route("login")]
        [HttpPost]
        public async Task<UserAdmin> LogIn(LoginModel loginInfo)
        {
          return await _adminService.AdminLogIN(loginInfo);
         

        }


}

}