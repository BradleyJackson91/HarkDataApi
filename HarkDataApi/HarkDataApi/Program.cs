using HarkDataApi.BusinessLayer.Logic;
using HarkDataApi.DataAccessLayer.Data;
using HarkDataApi.DataAccessLayer.Repositories;
using HarkDataApi.ServiceLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Data Sources
string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

//IEnergyConsumptionDataSource a = new EnergyConsumptionDataSource(Path.Combine(rootPath, "HalfHourlyEnergyData.csv"));
//IEnergyConsumptionRepository ar = new EnergyConsumptionRepository(a);

//ITemperatureDataSource c = new TemperatureDataSource(Path.Combine(rootPath, "Weather.csv"));
//ITemperatureRepository cr = new TemperatureRepository(c);

//IEnergyConsumptionLogic al = new EnergyConsumptionLogic(ar, cr);
//IEnergyConsumptionService aser = new EnergyConsumptionService(al);


////IEnergyConsumptionAnomaliesDataSource b = new EnergyConsumptionAnomaliesDataSource(Path.Combine(rootPath, "HalfHourlyEnergyDataAnomalies.csv"));
////ITemperatureDataSource c = new TemperatureDataSource(Path.Combine(rootPath, "Weather.csv"));

builder.Services.AddSingleton<IEnergyConsumptionDataSource>(
    serviceProvider => new EnergyConsumptionDataSource(Path.Combine(rootPath, "HalfHourlyEnergyData.csv")));
builder.Services.AddSingleton<IEnergyConsumptionAnomaliesDataSource>(
    serviceProvider => new EnergyConsumptionAnomaliesDataSource(Path.Combine(rootPath, "HalfHourlyEnergyDataAnomalies.csv")));
builder.Services.AddSingleton<ITemperatureDataSource>(
    serviceProvider => new TemperatureDataSource(Path.Combine(rootPath, "Weather.csv")));
#endregion DataSources

#region Data Repositories
builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
builder.Services.AddScoped<IEnergyConsumptionAnomaliesRepository, EnergyConsumptionAnomaliesRepository>();
builder.Services.AddScoped<IEnergyConsumptionRepository, EnergyConsumptionRepository>();

#endregion Data Repositories

#region Business Logic Objects
builder.Services.AddScoped<IAnomaliesLogic, AnomaliesLogic>();
builder.Services.AddScoped<IEnergyConsumptionLogic, EnergyConsumptionLogic>();
//builder.Services.AddScoped<ITemperatureLogic, TemperatureLogic>(); //TODO: Uncomment this when implemented.
#endregion Business Logic Objects

#region Services
builder.Services.AddScoped<IEnergyConsumptionService, EnergyConsumptionService>();
//builder.Services.AddScoped<IAnomaliesService, AnomaliesService>(); // TODO: Uncomment this when implemented.
//builder.Services.AddScoped<ITemperatureService, TemperatureService>(); //TODO: Uncomment this when implemented.
#endregion Services

#region Swagger
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hark Data API", Version = "v1" });
});
#endregion Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
