using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly string _connectionString;

        public ComentarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            var comentarios = new List<Comentario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, FkIdUsuario, FkIdVideojuego, Titulo, Texto, Fecha, Valoracion, Likes, Dislikes FROM Comentarios";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        comentarios.Add(new Comentario
                        {
                            Id = reader.GetInt32(0),
                            FkIdUsuario = reader.GetInt32(1),
                            FkIdVideojuego = reader.GetInt32(2),
                            Titulo = reader.GetString(3),
                            Texto = reader.GetString(4),
                            Fecha = reader.GetDateTime(5),
                            Valoracion = reader.GetInt32(6),
                            Likes = reader.GetInt32(7),
                            Dislikes = reader.GetInt32(8)
                        });
                    }
                }
            }
            return comentarios;
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            Comentario? comentario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, FkIdUsuario, FkIdVideojuego, Titulo, Texto, Fecha, Valoracion, Likes, Dislikes FROM Comentarios WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            comentario = new Comentario
                            {
                                Id = reader.GetInt32(0),
                                FkIdUsuario = reader.GetInt32(1),
                                FkIdVideojuego = reader.GetInt32(2),
                                Titulo = reader.GetString(3),
                                Texto = reader.GetString(4),
                                Fecha = reader.GetDateTime(5),
                                Valoracion = reader.GetInt32(6),
                                Likes = reader.GetInt32(7),
                                Dislikes = reader.GetInt32(8)
                            };
                        }
                    }
                }
            }
            return comentario;
        }

        public async Task AddAsync(Comentario comentario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Comentarios (FkIdUsuario, FkIdVideojuego, Titulo, Texto, Fecha, Valoracion, Likes, Dislikes) VALUES (@FkIdUsuario, @FkIdVideojuego, @Titulo, @Texto, @Fecha, @Valoracion, @Likes, @Dislikes)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FkIdUsuario", comentario.FkIdUsuario);
                    command.Parameters.AddWithValue("@FkIdVideojuego", comentario.FkIdVideojuego);
                    command.Parameters.AddWithValue("@Titulo", comentario.Titulo);
                    command.Parameters.AddWithValue("@Texto", comentario.Texto);
                    command.Parameters.AddWithValue("@Fecha", comentario.Fecha);
                    command.Parameters.AddWithValue("@Valoracion", comentario.Valoracion);
                    command.Parameters.AddWithValue("@Likes", comentario.Likes);
                    command.Parameters.AddWithValue("@Dislikes", comentario.Dislikes);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Comentarios SET FkIdUsuario = @FkIdUsuario, FkIdVideojuego = @FkIdVideojuego, Titulo = @Titulo, Texto = @Texto, Fecha = @Fecha, Valoracion = @Valoracion, Likes = @Likes, Dislikes = @Dislikes WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", comentario.Id);
                    command.Parameters.AddWithValue("@FkIdUsuario", comentario.FkIdUsuario);
                    command.Parameters.AddWithValue("@FkIdVideojuego", comentario.FkIdVideojuego);
                    command.Parameters.AddWithValue("@Titulo", comentario.Titulo);
                    command.Parameters.AddWithValue("@Texto", comentario.Texto);
                    command.Parameters.AddWithValue("@Fecha", comentario.Fecha);
                    command.Parameters.AddWithValue("@Valoracion", comentario.Valoracion);
                    command.Parameters.AddWithValue("@Likes", comentario.Likes);
                    command.Parameters.AddWithValue("@Dislikes", comentario.Dislikes);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Comentarios WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
