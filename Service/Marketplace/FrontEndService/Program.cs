using FrontEndService.Config;
using FrontEndService.Manager;
using FrontEndService.Manager.Interface;
using FrontEndService.Messaging;
using FrontEndService.Service;
using FrontEndService.Service.Interface;
using FrontEndService.ViewModel.EndpointMap;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IAdminServiceManager, AdminServiceManager>();
builder.Services.AddScoped<IAuthServiceManager, AuthServiceManager>();
builder.Services.AddScoped<ISalesServiceManager, SalesServiceManager>();
builder.Services.AddScoped<IHttpManager, HttpManager>();

builder.Services.AddScoped<AdminMap>();
builder.Services.AddScoped<SalesMap>();
builder.Services.AddScoped<AuthMap>();


builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
// not sure if this is needed here this is also in AdminService along with the secret in appsettings look into removing JWTConfig from frontEndService
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    // Configure what type of Authentication we are going to use
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        // production would want to make below true
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = false
    };
});

var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod()
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors();

app.MapControllers();

app.Run();
