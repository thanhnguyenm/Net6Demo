// See https://aka.ms/new-console-template for more information

using Microsoft.Identity.Client;

const string _clientId = "cfc8f615-9694-40ac-a4ce-1230e43af6ee";
const string _tenantId = "81eb80c4-abc4-42e4-ae6b-8bcb225104b6";

var app = PublicClientApplicationBuilder
.Create(_clientId)
.WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
.WithRedirectUri("http://localhost")
.Build();

string[] scopes = { "user.read" };
AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

Console.WriteLine(result.AccessToken);

Console.WriteLine("Hello, World!");
