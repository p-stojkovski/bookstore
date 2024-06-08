using Bookstore.Books;
using Bookstore.Users;
using FastEndpoints;
using Serilog;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Reflection;
using Bookstore.OrderProcessing;
using Bookstore.SharedKernel;
using Bookstore.Users.UseCases.Cart.AddItem;
using Bookstore.EmailSending;

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
List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.Services.AddBooksModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddEmailSendingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUsersModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);

// Set up MediatR
builder.Services.AddMediatR(cfg
    => cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));
builder.Services.AddMediatRLoggingBehavior();
builder.Services.AddMediatRValidationBehavior();
builder.Services.AddValidatorsFromAssemblyContaining<AddItemToCartCommandValidator>();

// Add MediatR Domain Event Dispatcher
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();

public partial class Program { } // needed for tests
