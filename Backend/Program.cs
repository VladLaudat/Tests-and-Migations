using Backend.Service.Authentication;

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
builder.Services.AddScoped<IAuthentication, Authentication>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
