using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Videojuegos.DTOs;

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

        //función que coge los detalles del videojuego en base a un DTO
        public async Task<VideojuegoDetalleDto?> GetDetalleByIdAsync(int id)
        {
            VideojuegoDetalleDto? detalle = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT v.Titulo, v.Descripcion, v.AnioSalida, v.Pegi, v.Caratula, c.Nombre AS Compania,
                        (SELECT AVG(CAST(valoracion AS FLOAT)) FROM Comentarios WHERE fkIdVideojuego = v.Id) AS ValoracionPromedio
                    FROM Videojuegos v
                    JOIN Companias c ON v.fkIdCompania = c.Id
                    WHERE v.Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            detalle = new VideojuegoDetalleDto
                            {
                                Titulo = reader.GetString(0),
                                Descripcion = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                AnioSalida = reader.GetDateTime(2),
                                Pegi = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                                Caratula = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Compania = reader.GetString(5),
                                ValoracionPromedio = reader.IsDBNull(6) ? 0 : reader.GetDouble(6)
                            };
                        }
                    }
                }

                if (detalle != null)
                {
                    detalle.Generos = await GetGenerosByVideojuegoIdAsync(id, connection);
                    detalle.Plataformas = await GetPlataformasByVideojuegoIdAsync(id, connection);
                }
            }
            return detalle;
        }


        //función que coge todos los géneros de un videojuego en base al id del juego
        private async Task<List<string>> GetGenerosByVideojuegoIdAsync(int id, SqlConnection connection)
        {
            var generos = new List<string>();
            string query = @"
                SELECT g.Nombre FROM Generos g
                JOIN VideojuegoGenero vg ON g.Id = vg.fkIdGenero
                WHERE vg.fkIdVideojuego = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        generos.Add(reader.GetString(0));
                    }
                }
            }
            return generos;
        }

        //función que coge todos las plataformas en las que se encuentra disponible un videojuego en base al id del juego
        private async Task<List<string>> GetPlataformasByVideojuegoIdAsync(int id, SqlConnection connection)
        {
            var plataformas = new List<string>();
            string query = @"
                SELECT p.Nombre FROM Plataformas p
                JOIN VideojuegoPlataforma vp ON p.Id = vp.fkIdPlataforma
                WHERE vp.fkIdVideojuego = @Id";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        plataformas.Add(reader.GetString(0));
                    }
                }
            }
            return plataformas;
        }

        public async Task<List<Videojuego>> FiltrarVideojuegosAsync(string? compania, string? genero, string? plataforma)
        {
            var videojuegos = new List<Videojuego>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT DISTINCT v.Id, v.Titulo, v.Descripcion, v.AnioSalida, v.Pegi, v.Caratula
                    FROM Videojuegos v
                    JOIN Companias c ON v.fkIdCompania = c.Id
                    LEFT JOIN VideojuegoGenero vg ON v.Id = vg.fkIdVideojuego
                    LEFT JOIN Generos g ON vg.fkIdGenero = g.Id
                    LEFT JOIN VideojuegoPlataforma vp ON v.Id = vp.fkIdVideojuego
                    LEFT JOIN Plataformas p ON vp.fkIdPlataforma = p.Id
                    WHERE 1=1";

                if (!string.IsNullOrEmpty(compania)) query += " AND c.Nombre RLIKE @Compania";
                if (!string.IsNullOrEmpty(genero)) query += " AND g.Nombre RLIKE @Genero";
                if (!string.IsNullOrEmpty(plataforma)) query += " AND p.Nombre RLIKE @Plataforma";

                using (var command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(compania)) command.Parameters.AddWithValue("@Compania", compania);
                    if (!string.IsNullOrEmpty(genero)) command.Parameters.AddWithValue("@Genero", genero);
                    if (!string.IsNullOrEmpty(plataforma)) command.Parameters.AddWithValue("@Plataforma", plataforma);

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
            }
            return videojuegos;
        }
    }
}
