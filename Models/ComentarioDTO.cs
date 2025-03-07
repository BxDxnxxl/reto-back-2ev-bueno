namespace Models
{
    public class ComentarioDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
        public int Valoracion { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public string UsuarioNombre { get; set; }
    }
}
