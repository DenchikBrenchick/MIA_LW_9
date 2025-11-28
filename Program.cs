using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// --- НОВА ЧАСТИНА ---

// 1. Отримуємо порт від Railway (або 8080 локально) на самому початку
var portValue = Environment.GetEnvironmentVariable("PORT") ?? "8080";

// 2. Явно налаштовуємо сервер Kestrel слухати цей порт на всіх мережевих інтерфейсах (0.0.0.0)
builder.WebHost.ConfigureKestrel(options =>
{
    // ListenAnyIP еквівалентно прослуховуванню http://0.0.0.0:PORT
    options.ListenAnyIP(int.Parse(portValue));
    Console.WriteLine($"Kestrel налаштовано слухати порт: {portValue}"); // Для відлагодження у логах
});

// --------------------

var app = builder.Build();

// Читаємо змінні середовища для логіки (якщо вони були додані в попередніх кроках)
var welcomeMessage = Environment.GetEnvironmentVariable("WELCOME_MESSAGE")
                     ?? "Привіт! Це дефолтне повідомлення.";
var studentName = Environment.GetEnvironmentVariable("STUDENT_NAME")
                  ?? "Невідомий студент";

// Ендпоінти
app.MapGet("/", () => welcomeMessage);
app.MapGet("/time", () => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
app.MapGet("/hello", () => new { message = "Лаба 9 виконана!", student = studentName });

// --- ВАЖЛИВА ЗМІНА В КІНЦІ ---
// Раніше було: app.Run($"http://0.0.0.0:{port}");
// Тепер ми просто запускаємо додаток, бо Kestrel вже налаштовано вище.
app.Run();