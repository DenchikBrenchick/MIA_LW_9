// Program.cs — мінімальний, але 100 % робочий для Railway
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// ОБОВ’ЯЗКОВО слухаємо порт, який дає Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

app.MapGet("/", () => "Привіт! Твій C# .NET 8 проект успішно розгорнуто на Railway!");
app.MapGet("/time", () => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
app.MapGet("/hello", () => new { message = "Лаба 9 виконана!", student = "Твоє ім’я" });

app.Run($"http://0.0.0.0:{port}");