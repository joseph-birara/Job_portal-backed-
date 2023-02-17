using web.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace web.Services;

public class EmployerService{
private readonly IMongoCollection<Employer> _employerCollection;
    public EmployerService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionUrl);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _employerCollection = database.GetCollection<Employer>(mongoDbSettings.Value.collectionThree);

    }

    public EmployerService()
    {
    }

    public async Task RegisterEmployer (Employer employerInfo){
    await _employerCollection.InsertOneAsync(employerInfo);
    return;
}

public async Task<List<Employer>> GetAllEmployers(){
    return await _employerCollection.Find(_ =>true).ToListAsync();
}
public async Task<Employer> Getprofile(string id){
    return  _employerCollection.Find(t =>t.Id==id).FirstOrDefault();
}
public async Task UpdateEmployer(string id, string name){
    FilterDefinition<Employer> filter = Builders<Employer>.Filter.Eq("Id",id);
    UpdateDefinition<Employer> update = Builders<Employer>.Update.Set<string>("name",name);
    
    await _employerCollection.UpdateOneAsync(filter,update);
   return ;
}
public async Task DeleteEmployersync(string id){
     FilterDefinition<Employer> filter = Builders<Employer>.Filter.Eq("Id",id);
     await _employerCollection.DeleteOneAsync(filter);
     return;
     

}
    public Task<Employer> EmployerLogin(LoginModel loginInfo){
   return Task.FromResult(_employerCollection.Find(t=> t.name == loginInfo.name && t.password == loginInfo.password).FirstOrDefault());   
   
   
}


}