using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using System.Text.Json;
using System.Text;
using SantaUrsula.API.Infrastructure.Json;
using SantaUrsula.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SantaUrsulaDB");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Falta ConnectionStrings:SantaUrsulaDB en la configuración. En desarrollo usa User Secrets; en producción usa la variable de entorno ConnectionStrings__SantaUrsulaDB.");
}

// Conexión con SQL Server
builder.Services.AddDbContext<SantaUrsulaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Servicios básicos de la API
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Usar camelCase en JSON y registrar convertidores para DateOnly
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<JwtTokenService>();

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("Falta Jwt:Key en la configuración. En desarrollo usa User Secrets; en producción usa la variable de entorno Jwt__Key.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// CORS básico para desarrollo (ajustar en producción)
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("LocalDev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();