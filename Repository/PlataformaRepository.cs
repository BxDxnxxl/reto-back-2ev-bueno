using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly string _connectionString;

        public PlataformaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Plataforma>> GetAllAsync()
        {
            var plataformas = new List<Plataforma>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre FROM Plataformas";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        plataformas.Add(new Plataforma
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            return plataformas;
        }

        public async Task<Plataforma?> GetByIdAsync(int id)
        {
            Plataforma? plataforma = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre FROM Plataformas WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            plataforma = new Plataforma
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return plataforma;
        }

        public async Task AddAsync(Plataforma plataforma)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Plataformas (Nombre) VALUES (@Nombre)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", plataforma.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Plataforma plataforma)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Plataformas SET Nombre = @Nombre WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", plataforma.Id);
                    command.Parameters.AddWithValue("@Nombre", plataforma.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Plataformas WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
