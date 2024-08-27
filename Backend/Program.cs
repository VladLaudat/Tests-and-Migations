using Backend.DbContext;
using Backend.Service.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            builder.WithOrigins("http://frontend:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

//Authentication service
builder.Services.AddScoped<IAuthenticationSL, AuthenticationSL>();
builder.Services.AddScoped<IAuthenticationRL, AuthenticationRL>();

builder.Services.AddDbContext<BackendDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
