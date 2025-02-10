using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Username, Email, Contraseña, Nombre, Apellido1, Apellido2, ProfilePic FROM Usuarios";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            Contraseña = reader.GetString(3),
                            Nombre = reader.GetString(4),
                            Apellido1 = reader.GetString(5),
                            Apellido2 = reader.GetString(6),
                            ProfilePic = reader.GetString(7)
                        });
                    }
                }
            }
            return usuarios;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            Usuario? usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Username, Email, Contraseña, Nombre, Apellido1, Apellido2, ProfilePic FROM Usuarios WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2),
                                Contraseña = reader.GetString(3),
                                Nombre = reader.GetString(4),
                                Apellido1 = reader.GetString(5),
                                Apellido2 = reader.GetString(6),
                                ProfilePic = reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Usuarios (Username, Email, Contraseña, Nombre, Apellido1, Apellido2) VALUES (@Username, @Email, @Contraseña, @Nombre, @Apellido1, @Apellido2, @ProfilePic = ProfilePic)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", usuario.Username);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido1", usuario.Apellido1);
                    command.Parameters.AddWithValue("@Apellido2", usuario.Apellido2);
                    command.Parameters.AddWithValue("@ProfilePic", usuario.ProfilePic);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Usuarios SET Username = @Username, Email = @Email, Contraseña = @Contraseña, Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2, @ProfilePic = ProfilePic WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", usuario.Id);
                    command.Parameters.AddWithValue("@Username", usuario.Username);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido1", usuario.Apellido1);
                    command.Parameters.AddWithValue("@Apellido2", usuario.Apellido2);
                    command.Parameters.AddWithValue("@ProfilePic", usuario.ProfilePic);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Usuarios WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Usuario>> GetUsuariosByNombreAsync(string nombre)
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Username, Nombre, Apellido1, Apellido2, Contraseña, ProfilePic FROM Usuarios WHERE Nombre RLIKE @Nombre";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usuarios.Add(new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellido1 = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Apellido2 = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Contraseña = reader.GetString(5),
                                ProfilePic = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return usuarios;
        }
    }
}
