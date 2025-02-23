namespace Models
{
    public class UsuarioRolDto
    {
        public int UsuarioId { get; set; }
        public List<int> RolesIds { get; set; } = new List<int>();
    }
}
