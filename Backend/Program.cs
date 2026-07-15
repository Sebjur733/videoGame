using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Registers IgdbService in the dependency injection container 
// (Scoped = one instance per HTTP request)
builder.Services.AddScoped<IgdbService>();
builder.Services.AddScoped<UserService>();



builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

   


var app = builder.Build();
app.UseCors("FrontendPolicy");

app.UseCors("AllowReact");

app.MapControllers();


app.Run();