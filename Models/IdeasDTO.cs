namespace Models
{
    public class IdeaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreUsuario { get; set; }
    }
}
