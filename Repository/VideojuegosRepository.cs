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
                string query = @"
                    INSERT INTO Videojuegos (Titulo, Descripcion, AnioSalida, Pegi, Caratula, FkIdCompania) 
                    VALUES (@Titulo, @Descripcion, @AnioSalida, @Pegi, @Caratula, @FkIdCompania)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", videojuego.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", (object)videojuego.Descripcion ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AnioSalida", videojuego.AnioSalida);
                    command.Parameters.AddWithValue("@Pegi", (object)videojuego.Pegi ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Caratula", (object)videojuego.Caratula ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FkIdCompania", videojuego.FkIdCompania);
                    
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
                    detalle.Generos = await GetGenerosByVideojuegoIdAsync(id);
                    detalle.Plataformas = await GetPlataformasByVideojuegoIdAsync(id);
                }
            }
            return detalle;
        }


        //función que coge todos los géneros de un videojuego en base al id del juego
       public async Task<List<Genero>> GetGenerosByVideojuegoIdAsync(int videojuegoId)
        {
            var generos = new List<Genero>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT g.Id, g.Nombre
                    FROM Generos g
                    JOIN VideojuegoGenero vg ON g.Id = vg.fkIdGenero
                    WHERE vg.fkIdVideojuego = @fkIdVideojuego";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fkIdVideojuego", videojuegoId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            generos.Add(new Genero
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                            });
                        }
                    }
                }
            }

            return generos;
        }

        //función que coge todos las plataformas en las que se encuentra disponible un videojuego en base al id del juego
        public async Task<List<Plataforma>> GetPlataformasByVideojuegoIdAsync(int videojuegoId)
        {
            var plataformas = new List<Plataforma>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT p.Id, p.Nombre
                    FROM Plataformas p
                    JOIN VideojuegoPlataforma vp ON p.Id = vp.fkIdPlataforma
                    WHERE vp.fkIdVideojuego = @fkIdVideojuego";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fkIdVideojuego", videojuegoId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            plataformas.Add(new Plataforma
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                            });
                        }
                    }
                }
            }

            return plataformas;
        }


        //filtro para cuando queramos hacer los filtros superiores
        //podemos pasar parametros pero es opcional, es para ir haciendo los filtros mas completos si quieres filtrar por varios caracteres
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
                    //estas funciones detectan si el parametro es nulo y si es nulo no lo añade al filtro de la consulta
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

        public async Task<List<Videojuego>> BuscarVideojuegosAsync(string filtro)
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
                    WHERE v.Titulo RLIKE @Filtro
                    OR c.Nombre RLIKE @Filtro
                    OR g.Nombre RLIKE @Filtro
                    OR p.Nombre RLIKE @Filtro";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filtro", filtro);

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

        public async Task<List<Videojuego>> GetTop5MejoresVideojuegosValoradosAsync()
        {
            var videojuegos = new List<Videojuego>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT TOP 5 
                        v.Id, v.Titulo, v.Descripcion, v.AnioSalida, v.Pegi, v.Caratula,
                        AVG(CAST(c.valoracion AS FLOAT)) AS ValoracionPromedio
                    FROM Videojuegos v
                    LEFT JOIN Comentarios c ON v.Id = c.fkIdVideojuego
                    GROUP BY v.Id, v.Titulo, v.Descripcion, v.AnioSalida, v.Pegi, v.Caratula
                    ORDER BY ValoracionPromedio DESC";

                using (var command = new SqlCommand(query, connection))
                {
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
