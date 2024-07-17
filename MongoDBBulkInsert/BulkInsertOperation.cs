using MongoDB.Driver;
using Newtonsoft.Json;

namespace MongoDBBulkInsert;

public class BulkInsertOperation
{
    public MongoClient client { get; set; }
    public IMongoDatabase db { get; set; }
    public IMongoCollection<User> collection { get; set; }

    public BulkInsertOperation(string connectionString, string databaseName, string collectionName)
    {
        client = new MongoClient(connectionString);
        db = client.GetDatabase(databaseName);
        collection = db.GetCollection<User>(collectionName);
    }

    public void BulkInsert(string filePath)
    {
        var usersBatch = new List<User>();
        using var reader = new StreamReader(filePath);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            var user = JsonConvert.DeserializeObject<User>(line);
            usersBatch.Add(user);

            if (usersBatch.Count >= 1000)
            {
                collection.InsertMany(usersBatch);
                usersBatch.Clear();
            }
        }

        if (usersBatch.Count > 0)
        {
            collection.InsertMany(usersBatch);
        }

        Console.WriteLine($"Bulk insrt complete for file: {filePath}");
    }
}