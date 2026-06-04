using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

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
    new { name = "rockiking", role = "Tech Lead", fact = "Люблю C#" },
    new { name = "AndR3www", role = "Developer", fact = "Пишу на C# с 1 курса" },
    new { name = "Perry2016-89", role = "QA", fact = "Нахожу баги быстрее всех" },
};

app.MapGet("/api/team", () => Results.Ok(team));

app.Run();