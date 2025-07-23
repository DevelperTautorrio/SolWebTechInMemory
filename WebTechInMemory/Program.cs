using System.Text;
using APPLICATION.Interfaces;
using APPLICATION.Mappings;
using APPLICATION.Models;
using APPLICATION.Services;
using DOMAIN.ENTITIES;
using INFRASTRUCTURE;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- Basic Configuration ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- JWT Settings ---
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// --- Database Setup (In-Memory for development) ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AppInMemoryDB"));

// --- AutoMapper Configuration ---
builder.Services.AddAutoMapper(typeof(UserProfile));

// --- Application Services Registration ---
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IUserProfileService, ProfileServices>();

// --- Domain Services Registration ---
builder.Services.AddScoped<B_User>();
builder.Services.AddScoped<D_User>();

// --- JWT Authentication Setup ---
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

// --- Swagger/OpenAPI Configuration ---
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebTechInMemory API",
        Version = "v1",
        Description = "API for user management and authentication"
    });

    // JWT Bearer token configuration for Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// --- Middleware Pipeline Configuration ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebTechInMemory v1");
        c.RoutePrefix = "swagger";
        c.DisplayRequestDuration(); 
        c.EnableDeepLinking(); 
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();