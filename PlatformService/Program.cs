using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

if (environment.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMem"));
}

services.AddScoped<IPlatformRepository, PlatformRepository>();

services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopuldation(app, environment.IsProduction());

app.Run();
