using Project.Api.Extensions;
using Project.Application;
using Project.Infrastructure;
using Project.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();
app.UseApiServices();

app.Run();