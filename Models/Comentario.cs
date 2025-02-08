namespace Models;

public class Comentario
{
    public int Id { get; set; }
    public int FkIdUsuario { get; set; }
    public int FkIdVideojuego { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public DateTime Fecha { get; set; }
    public int Valoracion { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }

    public Comentario(int fkIdUsuario, int fkIdVideojuego, string titulo, string texto, DateTime fecha, int valoracion, int likes, int dislikes)
    {
        FkIdUsuario = fkIdUsuario;
        FkIdVideojuego = fkIdVideojuego;
        Titulo = titulo;
        Texto = texto;
        Fecha = fecha;
        Valoracion = valoracion;
        Likes = likes;
        Dislikes = dislikes;
    }

    public Comentario() { }
}
