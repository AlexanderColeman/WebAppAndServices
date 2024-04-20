using AdminService.Config;
using AdminService.Controllers.Filters;
using AdminService.Data;
using AdminService.Manager;
using AdminService.Manager.Interface;
using AdminService.Messaging;
using AdminService.Messaging.Interface;
using AdminService.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdminDbContext>(options => options.UseNpgsql(conn));
// incase email is not verified in order for you to log in toyr emial needs to be verifed
// override default config that existing in current identity provider - email doesnt need to be verified let the user log in 
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AdminDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAdminManager, AdminManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();
builder.Services.AddScoped<IEventDispatcher, EventDispatcher>();
builder.Services.AddScoped<EventDispatcherFilter>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));


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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
