using Microsoft.EntityFrameworkCore;
using PetsApi.Data;
using PetsApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectString = builder
    .Configuration
    .GetConnectionString("Animais") ?? "Data Source=Animais.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddSqlite<AnimalDb>(connectString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/animais", async (AnimalDb db) => await db.Animais.ToListAsync());

app.MapGet("/animal/{id}", async (AnimalDb db, int id) => await db.Animais.FindAsync(id));

app.MapPost("/animal", async (AnimalDb db, Animal animal) =>
{
    await db.Animais.AddAsync(animal);
    await db.SaveChangesAsync();
    return Results.Created($"/animal/{animal.Id}", animal);
});

app.MapPut("/animal/{id}", async (AnimalDb db, Animal animalUpdate, int id) =>
{
    var animal = await db.Animais.FindAsync(id);
    if (animal is null) return Results.NotFound();
    animal.Nome = animalUpdate.Nome;
    animal.Idade = animalUpdate.Idade;
    animal.Cor = animalUpdate.Cor;
    animal.Tipo = animalUpdate.Tipo;
    animal.Peso_kg = animalUpdate.Peso_kg;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/animal/{id}", async (AnimalDb db, int id) =>
{
    var animal = await db.Animais.FindAsync(id);
    if (animal is null) return Results.NotFound();
    db.Animais.Remove(animal);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
