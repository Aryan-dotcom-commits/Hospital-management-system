using Data.ApplicationDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", 
        policy => {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("database"));
});

// Add Cookie Authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/login"; // Redirect to login if unauthorized
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddHttpContextAccessor();

// Add authorization
builder.Services.AddAuthorization();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();


app.Run();