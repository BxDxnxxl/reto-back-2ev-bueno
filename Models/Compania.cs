namespace Models;

public class Compania
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string? UrlImagen { get; set; }

    public Compania(string nombre, string? urlImagen = null)
    {
        Nombre = nombre;
        UrlImagen = urlImagen;
    }

    public Compania() { }
}
