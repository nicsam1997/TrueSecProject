using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using TrueSecProject.Repositories;
using TrueSecProject.Settings;
using Microsoft.Extensions.Options;
using TrueSecProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrueSecProject.Swagger;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// MongoDB config
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    if (string.IsNullOrEmpty(settings.ConnectionString))
    {
        throw new ArgumentException("MongoDB connection string is not configured.");
    }
    var mongoClientSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
    return new MongoClient(mongoClientSettings);
});

builder.Services.AddScoped<IAuthorizedUserRepository, MongoAuthorizedUserRepository>();
builder.Services.AddScoped<IAuthorizedUserService, AuthorizedUserService>();
builder.Services.AddScoped<IVulnerabilityRepository, MongoVulnerabilityRepository>();
builder.Services.AddScoped<IVulnerabilityService, VulnerabilityService>();

// Authorization and Authentication config
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT key is not configured.")
            )
        )
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.OperationFilter<AuthOperationFilter>();
    // Populate the example values for the swagger documentation
    options.SchemaFilter<DefaultValuesSchemaFilter>();
    options.SchemaFilter<ExternalReferenceSchemaFilter>();
    options.SchemaFilter<GranularMarkingSchemaFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Vulnerability API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "Vulnerability API V1");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
