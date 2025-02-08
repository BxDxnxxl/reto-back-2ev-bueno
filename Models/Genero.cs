namespace Models;

public class Genero
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Genero(string nombre)
    {
        Nombre = nombre;
    }

    public Genero() { }
}
