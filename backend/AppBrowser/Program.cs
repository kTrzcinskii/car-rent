using System.Text;
using AppBrowser.Infrastructure;
using AppBrowser.Services.Implementations;
using AppBrowser.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<CarRentalExternalProviderService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IRentService, RentService>();

var connectionString = Environment.GetEnvironmentVariable("DB_CONN_STRING");

connectionString ??= builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<DataContext>(opt => opt.UseLazyLoadingProxies().UseNpgsql(connectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,  // You can set this to true if you want to validate the issuer
        ValidateAudience = false, // You can set this to true if you want to validate the audience
        ValidateLifetime = true,  // Ensure token is not expired
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Secret")!)),
    };
});

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireClaim("Role", "User").Build())
    .AddPolicy("RegistrationOnly", policy => policy.RequireClaim("CanFinishRegistration", "true"));

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

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors("AllowFrontend");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error during migrations.");
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
