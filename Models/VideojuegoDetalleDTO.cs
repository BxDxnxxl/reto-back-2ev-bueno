namespace Videojuegos.DTOs;

public class VideojuegoDetalleDto
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime AnioSalida { get; set; }
    public int? Pegi { get; set; }
    public string? Caratula { get; set; }
    public string Compania { get; set; }
    public List<string> Generos { get; set; }
    public List<string> Plataformas { get; set; }
    public double ValoracionPromedio { get; set; }

    public VideojuegoDetalleDto()
    {
        Generos = new List<string>();
        Plataformas = new List<string>();
        ValoracionPromedio = 0;
    }
}
