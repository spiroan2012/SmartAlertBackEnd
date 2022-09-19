using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Runtime;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using Api.Helpers;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<SmartAlertContext>(x =>
                x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
        o =>
        {
            o.UseNetTopologySuite();
        }));
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddScoped<IFirebaseService, FirebaseService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SmartAlertContext>();
    dataContext.Database.MigrateAsync().Wait();
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
