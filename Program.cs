using Videojuegos.Repositories;
using Videojuegos.Service;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VideojuegosDB");

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(provider =>
new UsuarioRepository(connectionString));
builder.Services.AddScoped<IRolRepository, RolRepository>(provider =>
new RolRepository(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
