using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAD"));

//or manually validate Jwt
//builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
//{
//    var existingOnTokenValidatedHandler = options.Events.OnTokenValidated;
//    options.Events.OnTokenValidated = async context =>
//    {
//        await existingOnTokenValidatedHandler(context);
//        // Your code to add extra configuration that will be executed after the current event implementation.
//        //options.TokenValidationParameters.ValidIssuers = new[] { /* list of valid issuers */ };
//        //options.TokenValidationParameters.ValidAudiences = new[] { /* list of valid audiences */};
//    };
//});

// manuall 2
//services.AddAuthentication()
//            .AddJwtBearer(AuthenticationSchemes.AzureB2CRopc, options =>
//            {
//                options.Authority = authority;
//                options.MetadataAddress = metadataRopcAddress;
//                options.Audience = audience;

//                options.Events = new JwtBearerEvents
//                {
//                    OnAuthenticationFailed = ctx =>
//                    {
//                        System.Diagnostics.Debug.WriteLine(ctx.Exception.ToString());

//                        return Task.CompletedTask;
//                    },
//                    OnTokenValidated = context =>
//                    {
//                        var accessToken = (System.IdentityModel.Tokens.Jwt.JwtSecurityToken)context.SecurityToken;

//                        System.Diagnostics.Debug.WriteLine($@"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString} {(accessToken.Payload.ContainsKey("name") ? accessToken.Payload["name"] : null)} {accessToken.ValidFrom} {accessToken.ValidTo} {accessToken.RawData}");

//                        return Task.CompletedTask;
//                    },
//                    OnMessageReceived = context =>
//                    {
//                        if (context.Request.Headers["Accept"] == "text/event-stream")
//                        {
//                            var at = context.Request.Query["access_token"];
//                            if (!string.IsNullOrEmpty(at))
//                                context.Token = at;
//                        }
//                        return Task.CompletedTask;
//                    }
//                };
//            })

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
