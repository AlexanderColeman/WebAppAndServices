using FrontEndService.Manager;
using FrontEndService.Manager.Interface;
using FrontEndService.Messaging;
using FrontEndService.Service;
using FrontEndService.Service.Interface;
using FrontEndService.ViewModel.EndpointMap;
using Microsoft.Extensions.Configuration;

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
