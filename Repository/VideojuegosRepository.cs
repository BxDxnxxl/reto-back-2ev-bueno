using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class VideojuegoRepository : IVideojuegoRepository
    {
        private readonly string _connectionString;

        public VideojuegoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Videojuego>> GetAllAsync()
        {
            var videojuegos = new List<Videojuego>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Titulo, Descripcion, AnioSalida, Pegi, Caratula FROM Videojuegos";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        videojuegos.Add(new Videojuego
                        {
                            Id = reader.GetInt32(0),
                            Titulo = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            AnioSalida = reader.GetDateTime(3),
                            Pegi = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                            Caratula = reader.IsDBNull(5) ? null : reader.GetString(5)
                        });
                    }
                }
            }
            return videojuegos;
        }

        public async Task<Videojuego?> GetByIdAsync(int id)
        {
            Videojuego? videojuego = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Titulo, Descripcion, AnioSalida, Pegi, Caratula FROM Videojuegos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            videojuego = new Videojuego
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                AnioSalida = reader.GetDateTime(3),
                                Pegi = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                                Caratula = reader.IsDBNull(5) ? null : reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return videojuego;
        }

        public async Task AddAsync(Videojuego videojuego)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Videojuegos (Titulo, Descripcion, AnioSalida, Pegi, Caratula) VALUES (@Titulo, @Descripcion, @AnioSalida, @Pegi, @Caratula)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", videojuego.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", (object)videojuego.Descripcion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AnioSalida", videojuego.AnioSalida);
                    command.Parameters.AddWithValue("@Pegi", (object)videojuego.Pegi ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Caratula", (object)videojuego.Caratula ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Videojuego videojuego)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Videojuegos SET Titulo = @Titulo, Descripcion = @Descripcion, AnioSalida = @AnioSalida, Pegi = @Pegi, Caratula = @Caratula WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", videojuego.Id);
                    command.Parameters.AddWithValue("@Titulo", videojuego.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", (object)videojuego.Descripcion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AnioSalida", videojuego.AnioSalida);
                    command.Parameters.AddWithValue("@Pegi", (object)videojuego.Pegi ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Caratula", (object)videojuego.Caratula ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Videojuegos WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
