using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Исправлен синтаксис настройки CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

var team = new[]
{
    new { name = "Иванов Иван", role = "Tech Lead", fact = "Люблю C#" },
    new { name = "Петрова Мария", role = "Developer", fact = "Пишу на C# с 1 курса" },
    new { name = "Сидоров Алексей", role = "QA", fact = "Нахожу баги быстрее всех" },
};

// Исправлена лямбда-стрелка => вместо />
app.MapGet("/api/team", () => Results.Ok(team));

// Исправлены лямбда-стрелки => во всем методе
app.MapGet("/api/team/{name}", (string name) =>
{
    var member = team.FirstOrDefault(m =>
        m.name.Contains(name, StringComparison.OrdinalIgnoreCase));

    return member is not null
        ? Results.Ok(member)
        : Results.NotFound(new { error = "Участник не найден" });
});

app.Run();
