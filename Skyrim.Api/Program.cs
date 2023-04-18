using Microsoft.EntityFrameworkCore;
using Serilog;
using Skyrim.Api.Configurations;
using Skyrim.Api.Data;
using Skyrim.Api.Domain;
using Skyrim.Api.Domain.DomainHelpers;
using Skyrim.Api.Domain.Interfaces;
using Skyrim.Api.Extensions.Interfaces;
using Skyrim.Api.Repository;
using Skyrim.Api.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
.WriteTo.Console()
.ReadFrom.Configuration(context.Configuration));

var connectionString = builder.Configuration.GetConnectionString("SkyrimDbConnectionString");

builder.Services.AddDbContext<SkyrimApiDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationDomain, LocationDomain>();
builder.Services.AddScoped<IRepositoryLoggerExtension, Skyrim.Api.Extensions.RepositoryLoggerExtensions>();
builder.Services.AddScoped<IDomainLoggerExtension, Skyrim.Api.Extensions.DomainLoggerExtensions>();
builder.Services.AddScoped<ILocationDtoFormatHelper, CreateLocationDtoFormatHelper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
