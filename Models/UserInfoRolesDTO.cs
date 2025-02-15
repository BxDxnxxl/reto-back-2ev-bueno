namespace Models;

public class UserInfoRoles
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public List<string> Roles { get; set; }

    public UserInfoRoles()
    {
        Roles = new List<string>();
    }
}
