namespace Models;

public class VideojuegoDetalleDto
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime AnioSalida { get; set; }
    public int? Pegi { get; set; }
    public string? Caratula { get; set; }
    public string Compania { get; set; }
    public List<Genero> Generos { get; set; }
    public List<Plataforma> Plataformas { get; set; }
    public double ValoracionPromedio { get; set; }

    public VideojuegoDetalleDto()
    {
        Generos = new List<Genero>();
        Plataformas = new List<Plataforma>();
        ValoracionPromedio = 0;
    }
}
