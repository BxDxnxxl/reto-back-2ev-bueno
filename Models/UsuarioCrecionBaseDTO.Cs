namespace Models;

public class UsuarioCreacionBaseDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Contrasenia { get; set; }

    public UsuarioCreacionBaseDto(string username, string email, string contrasenia, string nombre, string apellido1, string apellido2, string profilePic)
    {
        Username = username;
        Email = email;
        Contrasenia = contrasenia;
    }

    public UsuarioCreacionBaseDto() { }
}
