using Bluesoft.Bank.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Bluesoft.Bank.Services.IoC;

var builder = WebApplication.CreateBuilder(args);

var secrets = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

builder.Services.AddDbContext<BluesoftBankContext>(options
    => options.UseSqlServer("name=ConnectionStrings:BluesoftData"));

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
