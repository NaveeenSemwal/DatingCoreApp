using DatingApp.API.Middleware;
using DatingApp.Framework.Business;
using DatingApp.Framework.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Add Framework Registeration
DependencyInjectionConfiguration.ConfigureAppServices(builder.Services, builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x=>x.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200/", "https://localhost:4200/"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
