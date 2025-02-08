namespace Models;

public class Plataforma
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Plataforma(string nombre)
    {
        Nombre = nombre;
    }

    public Plataforma() { }
}
