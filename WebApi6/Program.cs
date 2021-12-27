using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApi6.Data;
using WebApi6.Handlers;
using WebApi6.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add connection to DbContext
builder.Services.AddDbContext<PatienDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IPatienRepository, PatienRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication("UserAuthenticationHandler")
    .AddScheme<AuthenticationSchemeOptions, UserAuthenticationHandler>("UserAuthenticationHandler", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
