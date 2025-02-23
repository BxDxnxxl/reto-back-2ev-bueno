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
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>(provider =>
new GeneroRepository(connectionString));
builder.Services.AddScoped<IPlataformaRepository, PlataformaRepository>(provider =>
new PlataformaRepository(connectionString));
builder.Services.AddScoped<IVideojuegoRepository, VideojuegoRepository>(provider =>
new VideojuegoRepository(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });


builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ICompaniaService, CompaniaService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPlataformaService, PlataformaService>();
builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();

var app = builder.Build();
{
app.UseSwagger();
app.UseSwaggerUI();
}
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers();

app.Run();
