namespace Models;

public class Videojuego
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime AnioSalida { get; set; }
    public int? Pegi { get; set; }
    public string? Caratula { get; set; }
    public int FkIdCompania { get; set; }

    public Videojuego(string titulo, string descripcion, DateTime anioSalida, int? pegi, string? caratula, int fkIdCompania)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        AnioSalida = anioSalida;
        Pegi = pegi;
        Caratula = caratula;
        FkIdCompania = fkIdCompania;
    }   

    public Videojuego() { }
}
