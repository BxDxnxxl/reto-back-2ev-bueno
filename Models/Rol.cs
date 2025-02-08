namespace Models;

public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Rol(string nombre)
    {
        Nombre = nombre;
    }

    public Rol() { }
}
