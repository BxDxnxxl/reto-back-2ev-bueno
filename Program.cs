using Videojuegos.Repositories;
using Videojuegos.Service;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VideojuegosDB");

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(provider =>
new UsuarioRepository(connectionString));
builder.Services.AddScoped<IRolRepository, RolRepository>(provider =>
new RolRepository(connectionString));
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>(provider =>
new ComentarioRepository(connectionString));
builder.Services.AddScoped<ICompaniaRepository, CompaniaRepository>(provider =>
new CompaniaRepository(connectionString));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ICompaniaService, CompaniaService>();

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
