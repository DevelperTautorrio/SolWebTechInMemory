using System.Text;
using APPLICATION.Interfaces;
using APPLICATION.Services;
using APPLICATION.Mappings;
using APPLICATION.Models;

using AutoMapper;

using DOMAIN.ENTITIES;
using INFRASTRUCTURE;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

// --- Database ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AppInMemoryDB"));

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(UserProfile)); 

// --- pplication ---
builder.Services.AddScoped<IUserService, UserServices>(); 
builder.Services.AddScoped<IAuthService, AuthServices>(); 
builder.Services.AddScoped<IUserProfileService, ProfileServices>();

// --- Business ---
builder.Services.AddScoped<B_User>(); 


builder.Services.AddScoped<D_User>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });







var app = builder.Build();


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
