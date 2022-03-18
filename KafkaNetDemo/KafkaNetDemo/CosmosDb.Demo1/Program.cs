// See https://aka.ms/new-console-template for more information
// Replace <documentEndpoint> with the information created earlier
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;

string cnnStr = "mongodb://demo-cosmosdb-learning:thYxvG4cMMl7tpJVCgyjZy3MAIHQvuRjQGPYatob3Rr1NJ3JtRetRIVsjaDekHyhw9wxxdIGU5KLvipbm03eUA==@demo-cosmosdb-learning.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@demo-cosmosdb-learning@";

// The Cosmos client instance
CosmosClient cosmosClient;

// The database we will create
Database database;

// The container we will create.
Container container;

// The names of the database and container we will create
string databaseId = "az204Database";
string containerId = "az204Container";

try
{
    //Console.WriteLine("Beginning operations...\n");
    //Core API
    cosmosClient = new CosmosClient("AccountEndpoint=https://my-demo-learning1.documents.azure.com:443/;AccountKey=hyhIb1tcPOeKJVzCHHr3GrIPcttnYiZVBryeluc8CVb52toMMrjGoXvNdOGFk1XVumhHC4R6NEAEiH4D3rLyWQ==;");

    ////MongoDb API
    ////MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(cnnStr));
    ////settings.SslSettings =
    ////  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
    ////var mongoClient = new MongoClient(settings);

    ////create database
    database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
    Console.WriteLine("Created Database: {0}\n", database.Id);

    ////// Create a new container
    container = await database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
    Console.WriteLine("Created Container: {0}\n", container.Id);

    ////Create a item
    var itemguid = Guid.NewGuid();
    await container.CreateItemAsync(new Person
    {
        id = itemguid.ToString(),
        ItemId = itemguid,
        FirstName = "Thanh",
        LastName = "Nguyen",
        Birthday = DateTimeOffset.UtcNow
    }, requestOptions: new ItemRequestOptions { PreTriggers = new string[] { "validateToDoItemTimestamp" } });

    Console.WriteLine("Created Item: {0}\n", itemguid);

    //call storeprocedure
    //using (var client = new DocumentClient(new Uri("https://my-demo-learning1.documents.azure.com:443/"), "hyhIb1tcPOeKJVzCHHr3GrIPcttnYiZVBryeluc8CVb52toMMrjGoXvNdOGFk1XVumhHC4R6NEAEiH4D3rLyWQ=="))
    //{
    //    //Execute Store Procedure  
    //    var result = await client.ExecuteStoredProcedureAsync<string>(UriFactory.CreateStoredProcedureUri(databaseId, containerId, "helloProc"),
    //        new Microsoft.Azure.Documents.Client.RequestOptions { PartitionKey = new Microsoft.Azure.Documents.PartitionKey(null) });
    //    Console.WriteLine($"Executed Store Procedure: response:{result.Response}");
    //}


    //var itemguid = Guid.NewGuid();
    //var newItem = new Person
    //{
    //    id = itemguid.ToString(),
    //    ItemId = itemguid,
    //    FirstName = "Thanh",
    //    LastName = "Nguyen",
    //    //Birthday = DateTimeOffset.UtcNow
    //};

    //using (var client = new DocumentClient(new Uri("https://my-demo-learning1.documents.azure.com:443/"), "hyhIb1tcPOeKJVzCHHr3GrIPcttnYiZVBryeluc8CVb52toMMrjGoXvNdOGFk1XVumhHC4R6NEAEiH4D3rLyWQ=="))
    //{
    //    //Execute Store Procedure  
    //    var result = await client.ExecuteStoredProcedureAsync<string>(UriFactory.CreateStoredProcedureUri(databaseId, containerId, "createSample"),
    //        new Microsoft.Azure.Documents.Client.RequestOptions { PartitionKey = new Microsoft.Azure.Documents.PartitionKey(newItem.LastName) }, newItem);
    //    Console.WriteLine($"Executed Store Procedure: response:{result.Response}");
    //}

}
catch (CosmosException de)
{
    Exception baseException = de.GetBaseException();
    Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
}
catch (Exception e)
{
    Console.WriteLine("Error: {0}", e);
}
finally
{
    Console.WriteLine("End of program, press any key to exit.");
    Console.ReadKey();
}

[Serializable]
class Person
{
    public string id { get; set; }
    public Guid ItemId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset? Birthday { get; set; }
}