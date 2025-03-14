namespace Models
{
    public class Ideas
    {
        public int Id { get; set; }
        public int FkIdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        
    }
}
