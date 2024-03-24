using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
//https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/blob/master/2-WebApp-graph-user/2-1-Call-MSGraph/README.md
//demo for MVC web using implicit flow
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

var scopes = builder.Configuration.GetValue<string>("DownstreamApi:Scopes").Split(' ');

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
//                .EnableTokenAcquisitionToCallDownstreamApi(scopes)
//                .AddInMemoryTokenCaches();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
