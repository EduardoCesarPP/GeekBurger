global using GeekBurger.Shared;
global using GeekBurger.Shared.Model;
global using GeekBurger.Shared.ExternalRepositories;
using GeekBurger.Shared.Extensions;
using GeekBurger.Products.Contract.Repository;
using GeekBurger.Products.Contract.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GeekBurger.Shared.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductsDbContext>(o => o.UseInMemoryDatabase("geekburger-products"), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
builder.Services.AddSingleton<IStoreRepository, StoresRepository>();
builder.Services.AddSingleton<ILogService, LogService>();
builder.Services.AddSingleton<IOrderChangedService, OrderChangedService>();
builder.Services.AddSingleton<IOrderRepository, OrdersRepository>();
builder.Services.AddSingleton<IPaymentRepository, PaymentsRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var mvcCoreBuilder = builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Version = "v1" }); });

builder.Services.AddHostedService<OrderBackgroundService>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders");
    });
}

var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("geekburger-products");
var context = new ProductsDbContext(optionsBuilder.Options);
context.Seed();

void Configure(IApplicationBuilder app, ProductsDbContext productsDbContext)
{
    productsDbContext.Seed();
}


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
