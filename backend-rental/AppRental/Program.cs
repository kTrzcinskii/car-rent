using AppRental.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AppRental.Services.Interfaces;
using AppRental.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AppRental.Infrastructure.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Workers";
    options.DefaultChallengeScheme = "Workers";
})
.AddJwtBearer("EmailLink", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Query.ContainsKey("token"))
            {
                context.Token = context.Request.Query["token"];
            }
            return Task.CompletedTask;
        }
    };
})
.AddJwtBearer("Workers", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    // No special token retrieval logic; defaults to the Authorization header
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder("Workers")
        .RequireAuthenticatedUser()
        .Build();
        
    options.AddPolicy("EmailLink", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AuthenticationSchemes.Add("EmailLink");
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DB_CONN_STRING");

connectionString ??= builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<DataContext>(opt => opt.UseLazyLoadingProxies().UseNpgsql(connectionString));

var azureBlobString = Environment.GetEnvironmentVariable("AZUREBLOB_CONN_STRING");

azureBlobString ??= builder.Configuration["AzureBlob:ConnectionString"];

builder.Services.AddAzureClients(azureBuilder => azureBuilder.AddBlobServiceClient(azureBlobString));

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
            policy.WithOrigins(allowedOrigins!)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// updates or creates the database on startup
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error during migraiton");
}

app.Run();