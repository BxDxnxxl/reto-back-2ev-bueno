using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly string _connectionString;

        public GeneroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Genero>> GetAllAsync()
        {
            var generos = new List<Genero>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre, url_imagen FROM Generos";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        generos.Add(new Genero
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            UrlImagen = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }
            return generos;
        }

        public async Task<Genero?> GetByIdAsync(int id)
        {
            Genero? genero = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre, url_imagen FROM Generos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            genero = new Genero
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                UrlImagen = reader.IsDBNull(2) ? null : reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return genero;
        }

        public async Task AddAsync(Genero genero)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Generos (Nombre, url_imagen) VALUES (@Nombre, @UrlImagen)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", genero.Nombre);
                    command.Parameters.AddWithValue("@UrlImagen", (object?)genero.UrlImagen ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Genero genero)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Generos SET Nombre = @Nombre, url_imagen = @UrlImagen WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", genero.Id);
                    command.Parameters.AddWithValue("@Nombre", genero.Nombre);
                    command.Parameters.AddWithValue("@UrlImagen", (object?)genero.UrlImagen ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Generos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Genero>> GetTop5GenresAsync()
        {
            var generos = new List<Genero>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT TOP 5 g.id, g.nombre AS Genero, g.url_imagen 
                    FROM Generos g
                    JOIN VideojuegoGenero vg ON g.id = vg.fkIdGenero
                    JOIN Videojuegos v ON vg.fkIdVideojuego = v.id
                    JOIN Comentarios c ON v.id = c.fkIdVideojuego
                    GROUP BY g.id, g.nombre, g.url_imagen
                    ORDER BY AVG(c.valoracion) DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        generos.Add(new Genero
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            UrlImagen = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }
            return generos;
        }
    }
}
