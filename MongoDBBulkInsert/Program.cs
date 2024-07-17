
using MongoDBBulkInsert;
using System.Diagnostics;

var CONNECTION_STRING = "mongodb://localhost:27017";
var DATABASE_NAME = "users";
var COLLECTION_NAME = "users";
var USERS_FILES_DIRECTORY = "C:/Users/Ido/Documents/GitHub/user-search/user-generator/users";
var USER_FILES_AMOUNT = 10;

var bulkInert = new BulkInsertOperation(CONNECTION_STRING, DATABASE_NAME, COLLECTION_NAME);

var timer = new Stopwatch();

timer.Start();
for (var fileNumber = 0; fileNumber < USER_FILES_AMOUNT; fileNumber++)
{
    var filePath = $"{USERS_FILES_DIRECTORY}/users{fileNumber}.jsonl";

    bulkInert.BulkInsert(filePath);
}
timer.Stop();

Console.WriteLine($"Timer: {timer.ToString()}");