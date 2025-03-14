using Videojuegos.Repositories;
using Videojuegos.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Videojuegos.Services;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión desde configuración
var connectionString = builder.Configuration.GetConnectionString("VideojuegosDB")
    ?? throw new InvalidOperationException("Database connection string is missing.");

// Obtener la clave secreta para JWT desde la configuración
var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"] 
    ?? throw new InvalidOperationException("JWT Secret Key is missing in configuration.");

var key = Encoding.UTF8.GetBytes(jwtSecretKey);

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

// Inyección de dependencias de los repositorios
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
builder.Services.AddScoped<ITokenRepository, TokenRepository>(provider =>
    new TokenRepository(connectionString));
builder.Services.AddScoped<IIdeaRepository, IdeaRepository>(provider =>
    new IdeaRepository(connectionString));

// Inyección del TokenService con la clave secreta
builder.Services.AddScoped<TokenService>(provider =>
{
    var tokenRepository = provider.GetRequiredService<ITokenRepository>();
    return new TokenService(tokenRepository, jwtSecretKey);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS para permitir todas las solicitudes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Inyección de dependencias de los servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ICompaniaService, CompaniaService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPlataformaService, PlataformaService>();
builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();
builder.Services.AddScoped<IIdeaService, IdeaService>();

var app = builder.Build();

// Configuración del middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAllOrigins");

app.UseAuthentication(); // Habilitar autenticación JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
