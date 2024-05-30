using Bookstore.Books;
using Bookstore.Users;
using FastEndpoints;
using Serilog;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) 
    => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(x => x.SigningKey = builder.Configuration["Auth:JwtSecret"]!)
    .AddAuthorization()
    .SwaggerDocument();

// Add module service
builder.Services.AddBooksModuleServices(builder.Configuration, logger);
builder.Services.AddUsersModuleServices(builder.Configuration, logger);

var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();

public partial class Program { } // needed for tests
