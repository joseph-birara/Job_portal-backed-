using web.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace web.Services;

public class AdminService{
private readonly IMongoCollection<UserAdmin> _adminCollection;
private readonly ServiceProviderService  _myService;
private readonly EmployerService  _allEmployers;
    private List<UserAdmin> tmp;

    public AdminService(IOptions<MongoDbSettings> mongoDbSettings)

    {
        _myService = new ServiceProviderService();
        _allEmployers = new EmployerService();
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionUrl);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _adminCollection = database.GetCollection<UserAdmin>(mongoDbSettings.Value.collectionTwo);

    }
public async Task AddAdmin (UserAdmin UserInformation){
    await _adminCollection.InsertOneAsync(UserInformation);
    return;
}

public async Task<List<UserAdmin>> GetAllAdmins(string id){
   
   
    var adm =  _adminCollection.Find(t=> t.Id == id).FirstOrDefault();
    if (adm.isSuper == true){
        tmp= _adminCollection.Find(_ =>true).ToList();

    }
    return tmp;
    
   
}
public async Task<List<Employer>> GetAllEmployers(){
   
   
    return await  _allEmployers.GetAllEmployers();

    
    
   
}
public async Task UpdateAdmin(string id, string name){
    FilterDefinition<UserAdmin> filter = Builders<UserAdmin>.Filter.Eq("Id",id);
    UpdateDefinition<UserAdmin> update = Builders<UserAdmin>.Update.Set<string>("name",name);
    
    await _adminCollection.UpdateOneAsync(filter,update);
   return ;
}
public async Task DeleteAdminAsync(string id){
     FilterDefinition<UserAdmin> filter = Builders<UserAdmin>.Filter.Eq("Id",id);
     await _adminCollection.DeleteOneAsync(filter);
     return;
     

}
    public Task<UserAdmin> AdminLogIN(LoginModel loginInfo){
   return Task.FromResult(_adminCollection.Find(t=> t.name == loginInfo.name && t.password == loginInfo.password ).FirstOrDefault());   
   
   
}
public async Task<List<UserInformation>> GetServicProviders(){
    
    return await _myService.GetAsync();
}


}