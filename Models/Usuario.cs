namespace Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Contrasenia { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string? ProfilePic{get; set;}

    public Usuario(string username, string email, string contrasenia, string nombre, string apellido1, string apellido2, string profilePic)
    {
        Username = username;
        Email = email;
        Contrasenia = contrasenia;
        Nombre = nombre;
        Apellido1 = apellido1;
        Apellido2 = apellido2;
        ProfilePic = profilePic;
    }

    public Usuario() { }
}
