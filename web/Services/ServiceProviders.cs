using web.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace web.Services;
public class ServiceProviderService{
    private readonly IMongoCollection<UserInformation> _userCollection;
    public ServiceProviderService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionUrl);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<UserInformation>(mongoDbSettings.Value.collectionOne);

    }

    public ServiceProviderService()
    {
    }

    public async Task CreateAsync (UserInformation UserInformation){
    await _userCollection.InsertOneAsync(UserInformation);
    return;
}

public async Task<List<UserInformation>> GetAsync(){
    return await _userCollection.Find(_ =>true).ToListAsync();
}
public async Task AddToItems(string id, string name){
    FilterDefinition<UserInformation> filter = Builders<UserInformation>.Filter.Eq("Id",id);
    UpdateDefinition<UserInformation> update = Builders<UserInformation>.Update.Set<string>("name",name);
    
    await _userCollection.UpdateOneAsync(filter,update);
   return ;
}
public async Task DeleteAsync(string id){
     FilterDefinition<UserInformation> filter = Builders<UserInformation>.Filter.Eq("Id",id);
     await _userCollection.DeleteOneAsync(filter);
     return;
     

}
    public  Task<UserInformation> ServiceProviderLogin(LoginModel loginInfo){
   return  Task.FromResult(_userCollection.Find(t=> t.name == loginInfo.name && t.password == loginInfo.password).FirstOrDefault());   
   
   
}




}
