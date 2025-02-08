namespace Models;

public class Videojuego
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime AnioSalida { get; set; }
    public int? Pegi { get; set; }
    public string? Caratula { get; set; }

    public Videojuego(string titulo, string descripcion, DateTime anioSalida, int? pegi, string? caratula)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        AnioSalida = anioSalida;
        Pegi = pegi;
        Caratula = caratula;
    }

    public Videojuego() { }
}
