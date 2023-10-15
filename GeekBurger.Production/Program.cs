using AutoMapper;
using GeekBurger.Production.Contract.Repository;
using GeekBurger.Production.Contract.Service;
using GeekBurger.Shared.Service;
using GeekBurger.Production.Helper;

var builder = WebApplication.CreateBuilder(args);

// Register a hosted service to process messages from the queue

builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddSingleton<IProductionService, ProductionService>();
builder.Services.AddSingleton<IProductionRepository, ProductionRepository>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProductionAutomapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddHostedService<ProductionBackgroundService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
