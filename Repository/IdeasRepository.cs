using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly string _connectionString;

        public IdeaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Ideas>> GetAllAsync()
        {
            var ideas = new List<Ideas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, FkIdUsuario, Titulo, Descripcion, Tipo, FechaCreacion FROM Ideas";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ideas.Add(new Ideas
                        {
                            Id = reader.GetInt32(0),
                            FkIdUsuario = reader.GetInt32(1),
                            Titulo = reader.GetString(2),
                            Descripcion = reader.GetString(3),
                            Tipo = reader.GetString(4),
                            FechaCreacion = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return ideas;
        }


        public async Task AddAsync(Ideas idea)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Ideas (FkIdUsuario, Titulo, Descripcion, Tipo, FechaCreacion) VALUES (@FkIdUsuario, @Titulo, @Descripcion, @Tipo, @FechaCreacion)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FkIdUsuario", idea.FkIdUsuario);
                    command.Parameters.AddWithValue("@Titulo", idea.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", idea.Descripcion);
                    command.Parameters.AddWithValue("@Tipo", idea.Tipo);
                    command.Parameters.AddWithValue("@FechaCreacion", idea.FechaCreacion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Ideas WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<IdeaDto>> GetAllIdeasConNombreUsuarioAsync()
{
    var ideas = new List<IdeaDto>();

    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();
        string query = @"
            SELECT i.Id, i.Titulo, i.Descripcion, i.Tipo, i.FechaCreacion, u.username
            FROM Ideas i
            JOIN Usuarios u ON i.FkIdUsuario = u.Id
            ORDER BY i.FechaCreacion DESC";

        using (var command = new SqlCommand(query, connection))
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    ideas.Add(new IdeaDto
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Descripcion = reader.GetString(2),
                        Tipo = reader.GetString(3),
                        FechaCreacion = reader.GetDateTime(4),
                        NombreUsuario = reader.GetString(5)
                    });
                }
            }
        }
    }
    return ideas;
}

        
    }
}
