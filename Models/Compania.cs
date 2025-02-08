namespace Models;

public class Compania
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Compania(string nombre)
    {
        Nombre = nombre;
    }

    public Compania() { }
}
