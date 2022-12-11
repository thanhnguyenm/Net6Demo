// See https://aka.ms/new-console-template for more information
using Microsoft.Identity.Client;

var _clientId = "";//"de92701e-6b30-427e-b2bd-5d895f02ac72";
var _tenantId = "";// "9091782d-c600-47a5-9d3c-4c11ddeb7628";



var app = PublicClientApplicationBuilder
    .Create(_clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
    .WithRedirectUri("http://localhost")
    .Build();

string[] scopes = { "user.read" };

AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
Console.WriteLine($"Token {result.AccessToken}");
