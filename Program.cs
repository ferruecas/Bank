using Bank.Models;
using Bank.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {

        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
}

);
builder.Services.AddScoped<ITransaccionService, TransaccionService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddSqlServer<BluesoftBankDbContext>("Server = (local); Database = BancoDB; Trusted_Connection = True; TrustServerCertificate = true");


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
