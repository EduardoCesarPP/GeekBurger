using GeekBurger.Products.Contract.Repository;
using GeekBurger.StoreCatalogs.Contract.Repository;
using GeekBurger.StoreCatalogs.Contract.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductsDbContext>(o => o.UseInMemoryDatabase("geekburger-products"));
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IStoreCatalogService, StoreCatalogService>();
builder.Services.AddScoped<IStoreCatalogRepository, StoreCatalogRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var mvcCoreBuilder = builder.Services.AddMvc();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store Catalogs", Version = "v1" }); });

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store Catalogs");
    });
}

var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase("geekburger-products");
var context = new ProductsDbContext(optionsBuilder.Options);
context.Seed();

void Configure(IApplicationBuilder app, ProductsDbContext

productsDbContext)

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
