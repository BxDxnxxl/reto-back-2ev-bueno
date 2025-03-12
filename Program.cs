using Videojuegos.Repositories;
using Videojuegos.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Videojuegos.Services;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("VideojuegosDB")
    ?? throw new InvalidOperationException("Database connection string is missing.");

var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"] 
    ?? throw new InvalidOperationException("JWT Secret Key is missing in configuration.");

var key = Encoding.UTF8.GetBytes(jwtSecretKey);

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

builder.Services.AddScoped<TokenService>(provider =>
{
    var tokenRepository = provider.GetRequiredService<ITokenRepository>();
    return new TokenService(tokenRepository, jwtSecretKey);
});

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

app.Logger.LogInformation("Servidor ASP.NET Core iniciado correctamente");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Videojuegos V1");
    c.RoutePrefix = string.Empty;
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (!app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("No se usará HTTPS dentro del contenedor.");
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
