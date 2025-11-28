using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

var portValue = Environment.GetEnvironmentVariable("PORT") ?? "8880";


builder.WebHost.ConfigureKestrel(options =>
{

    options.ListenAnyIP(int.Parse(portValue));
    Console.WriteLine($"Kestrel налаштовано слухати порт: {portValue}"); 
});



var app = builder.Build();


var welcomeMessage = Environment.GetEnvironmentVariable("WELCOME_MESSAGE")
                     ?? "Привіт! Це дефолтне повідомлення.";
var studentName = Environment.GetEnvironmentVariable("STUDENT_NAME")
                  ?? "Невідомий студент";

app.MapGet("/", () => welcomeMessage);
app.MapGet("/time", () => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
app.MapGet("/hello", () => new { message = "Лаба 9 виконана!", student = studentName });


app.Run();