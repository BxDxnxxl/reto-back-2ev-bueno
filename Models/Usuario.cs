namespace Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Contraseña { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }

    public Usuario(string username, string email, string contraseña, string nombre, string apellido1, string apellido2)
    {
        Username = username;
        Email = email;
        Contraseña = contraseña;
        Nombre = nombre;
        Apellido1 = apellido1;
        Apellido2 = apellido2;
    }

    public Usuario() { }
}
