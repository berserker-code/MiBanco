using MiBanco.Application.Servicios;
using MiBanco.Domain.Interfaces;
using MiBanco.Infraestructure.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("MiBancoDb");

builder.Services.AddScoped<IClienteRepositorio>(sp => new ClienteRepositorio(connectionString));
builder.Services.AddScoped<ICuentaRepositorio>(sp => new CuentaRepositorio(connectionString));
builder.Services.AddScoped<ITransaccionRepositorio>(sp => new TransaccionRepositorio(connectionString));

builder.Services.AddScoped<ClienteServicio>(sp =>
    new ClienteServicio(
        sp.GetRequiredService<IClienteRepositorio>(),
        sp.GetRequiredService<ICuentaRepositorio>(),
        connectionString
        )
    );

builder.Services.AddScoped<TransaccionServicio>(sp =>
    new TransaccionServicio(
        sp.GetRequiredService<ICuentaRepositorio>(),
        sp.GetRequiredService<ITransaccionRepositorio>(),
        connectionString
        )
    );

builder.Services.AddScoped<CuentaServicio>(sp =>
new CuentaServicio(
    sp.GetRequiredService<ICuentaRepositorio>()
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
