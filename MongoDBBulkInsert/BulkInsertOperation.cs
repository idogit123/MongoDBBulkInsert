using MongoDB.Driver;

namespace MongoDBBulkInsert;

public class BulkInsertOperation
{
    public MongoClient client { get; set; }
    public IMongoDatabase db { get; set; }
    public string collection { get; set; }

    public BulkInsertOperation(string connectionString, string databaseName, string collectionName)
    {
        client = new MongoClient(connectionString);
        db = client.GetDatabase(databaseName);
        collection = db.GetCollection<User>(collectionName);
    }
}
