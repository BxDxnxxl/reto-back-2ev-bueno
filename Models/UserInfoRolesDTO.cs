namespace Models;

public class UserInfoRoles
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Email { get; set; }
    public string? Contrasenia { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public string? ProfilePic { get; set; }
    public List<Rol> Roles { get; set; }

    public UserInfoRoles()
    {
        Roles = new List<Rol>();
    }
}
