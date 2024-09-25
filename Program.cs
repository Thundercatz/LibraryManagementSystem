using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using LibraryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register application services
builder.Services.AddSingleton<ILibraryRepository, InMemoryLibraryRepository>();
builder.Services.AddTransient<ILibraryClient, LibraryClient>();
builder.Services.AddTransient<ILibraryOperator, LibraryOperator>();

builder.Services.AddTransient<IDbSeeder, DbSeeder>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Seed the database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
    seeder.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
