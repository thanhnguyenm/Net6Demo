// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.Cosmos;

//Console.WriteLine("Hello, World!");

var endPointUrl = "https://az204cosmosdemo002.documents.azure.com";
var primaryKey = "eC63DUGHpycUQkeEwiy30sIfAyzi9cKSHYN9AEZMaQgTUWlebVvLBrWLwrU7IscXX452DoXHI0K7ACDbMiLnoQ==";

var cosmosClient = new CosmosClient(endPointUrl, primaryKey);


var databaseid = "sample62fb48c0";//$"sample{Guid.NewGuid().ToString().Substring(0, 8)}";
Console.WriteLine($"Creating database {databaseid}");
var databaseReponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseid);

var database = databaseReponse.Database;

//Create container
Console.WriteLine("Creating database demo1");
var containerReponse = await database.CreateContainerIfNotExistsAsync("demo1", "/partitionKey");
var container = containerReponse.Container;

Console.WriteLine("Press Enter to next");
Console.ReadLine();

var rd = new Random();

Console.WriteLine("Creating new items");

for (int i = 0; i < 20; i++)
{
    Product item = new(
        id: Guid.NewGuid().ToString(),
        category: "gear-surf-surfboards",
        name: Guid.NewGuid().ToString(),
        quantity: rd.Next(100),
        sale: true,
        partitionKey: "gear-surf-surfboards",
        createdDateTime: null,
        updatedDateTime: null
    );

    Console.WriteLine($"------> Creating item {item.id}");

    await container.CreateItemAsync(item);

}

Console.WriteLine("Press Enter to next");
Console.ReadLine();

var itemId = "581d4010-0c17-46d4-b4e4-3eaf7e4c4155";
var existingItem = await container.ReadItemAsync<Product>(itemId, new PartitionKey("gear-surf-surfboards"));
if (existingItem != null && existingItem.Resource != null)
{
    Console.WriteLine($"itemid {existingItem.Resource.id} Name {existingItem.Resource.name}");
}

Console.WriteLine("Press Enter to next");
Console.ReadLine();

Console.WriteLine("Query items have quantity < 50");
var query = "Select * From demo1 p where p.quantity <50";
using var feed = container.GetItemQueryIterator<Product>(query);
while(feed.HasMoreResults)
{
    var response = await feed.ReadNextAsync();
    foreach(Product p in response)
    {
        Console.WriteLine($"itemid {p.id} Name {p.name}");
    }
}

Console.WriteLine("Press Enter to next");
Console.ReadLine();

// definitions
public record Product(
    string id,
    string category,
    string name,
    int quantity,
    bool sale,
    string partitionKey,
    DateTime? createdDateTime,
    DateTime? updatedDateTime
);