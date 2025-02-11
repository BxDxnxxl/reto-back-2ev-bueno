namespace Models;

public class LoginDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Nombre { get; set; }
    public List<string> Roles { get; set; }

    public LoginDto()
    {
        Roles = new List<string>();
    }
}
