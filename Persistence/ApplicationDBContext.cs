using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Persistence;

public class ApplicationDBContext
{
    private readonly IMongoDatabase _database;

    public ApplicationDBContext(IConfiguration configuration)
    {
        _database = new MongoClient(configuration["MongoDB:ConnectionString"])
            .GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName) =>
        _database.GetCollection<T>(collectionName);
}
