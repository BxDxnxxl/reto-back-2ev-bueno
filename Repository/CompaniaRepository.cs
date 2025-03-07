using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class CompaniaRepository : ICompaniaRepository
    {
        private readonly string _connectionString;

        public CompaniaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Compania>> GetAllAsync()
        {
            var companias = new List<Compania>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre, url_imagen FROM Companias";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        companias.Add(new Compania
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            UrlImagen = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }
            return companias;
        }

        public async Task<Compania?> GetByIdAsync(int id)
        {
            Compania? compania = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre, url_imagen FROM Companias WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            compania = new Compania
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                UrlImagen = reader.IsDBNull(2) ? null : reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return compania;
        }

        public async Task AddAsync(Compania compania)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Companias (Nombre, url_imagen) VALUES (@Nombre, @UrlImagen)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", compania.Nombre);
                    command.Parameters.AddWithValue("@UrlImagen", (object?)compania.UrlImagen ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Compania compania)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Companias SET Nombre = @Nombre, url_imagen = @UrlImagen WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", compania.Id);
                    command.Parameters.AddWithValue("@Nombre", compania.Nombre);
                    command.Parameters.AddWithValue("@UrlImagen", (object?)compania.UrlImagen ?? DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Companias WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        //función que saca las compañias más populares en nuestra web
        public async Task<List<Compania>> GetTop5CompaniesAsync()
        {
            var companias = new List<Compania>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT TOP 5 co.id, co.nombre AS compania, co.url_imagen 
                    FROM Companias co
                    JOIN Videojuegos v ON co.id = v.fkIdCompania
                    JOIN Comentarios c ON v.id = c.fkIdVideojuego
                    GROUP BY co.id, co.nombre, co.url_imagen
                    ORDER BY AVG(c.valoracion) DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        companias.Add(new Compania
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            UrlImagen = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }
            return companias;
        }
    }
}
