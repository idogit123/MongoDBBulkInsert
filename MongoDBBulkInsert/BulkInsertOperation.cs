using MongoDB.Driver;
using Newtonsoft.Json;

namespace MongoDBBulkInsert;

public class BulkInsertOperation
{
    private MongoClient client { get; set; }
    private IMongoDatabase db { get; set; }
    private IMongoCollection<User> collection { get; set; }

    public BulkInsertOperation(string connectionString, string databaseName, string collectionName)
    {
        client = new MongoClient(connectionString);
        db = client.GetDatabase(databaseName);
        collection = db.GetCollection<User>(collectionName);
    }

    private void BulkInsert(string filePath)
    {
        var usersBatch = new List<User>();
        using var reader = new StreamReader(filePath);
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            var user = JsonConvert.DeserializeObject<User>(line);
            if (user == null) { continue; }
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

    public void BulkInsertDirectory(string directoryPath)
    {
        for (var fileNumber = 0; fileNumber < GetDirectorySize(directoryPath); fileNumber++)
        {
            var filePath = $"{directoryPath}/users{fileNumber}.jsonl";

            BulkInsert(filePath);
        }
    }

    private int GetDirectorySize(string directoryPath)
    {
        return Directory.GetFiles(directoryPath).Length;
    }
}