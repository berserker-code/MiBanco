using MiBanco.Domain.Interfaces;
using MiBanco.Infraestructure.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("MiBancoDb");

builder.Services.AddScoped<IClienteRepositorio>(sp => new ClienteRepositorio(connectionString));
builder.Services.AddScoped<ICuentaRepositorio>(sp => new CuentaRepositorio(connectionString));
builder.Services.AddScoped<ITransaccionRepositorio>(sp => new TransaccionRepositorio(connectionString));


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
