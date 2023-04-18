using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

//Connection BD
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<MySqlContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<MySqlContext>
    (opt =>
builder.Services.AddScoped<IProductRepository, ProductRepository>());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping" });
});
var app = builder.Build();

builder.Services.AddAutoMapper(typeof(MappingConfig));//se der erro é aqui

//builder.Services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile<MappingConfig>();
//})));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
