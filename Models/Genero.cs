namespace Models;

public class Genero
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string? UrlImagen { get; set; }

    public Genero(string nombre, string? urlImagen = null)
    {
        Nombre = nombre;
        UrlImagen = urlImagen;
    }

    public Genero() { }
}
