using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly string _connectionString;

        public TokenRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserInfoRoles?> LoginAsync(LoginRequestDto authRequest)
        {
            UserInfoRoles? usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT u.Id, u.Username, u.Email, u.Nombre, u.Apellido1, u.Apellido2, u.ProfilePic
                    FROM Usuarios u
                    WHERE u.Username = @Username AND u.Contraseña = @Password";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", authRequest.Username);
                    command.Parameters.AddWithValue("@Password", authRequest.Password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new UserInfoRoles
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Email = reader.GetString(2),
                                Nombre = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Apellido1 = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Apellido2 = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                ProfilePic = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Roles = new List<Rol>()
                            };
                        }
                    }
                }

                if (usuario != null)
                {
                    usuario.Roles = await GetRolesByUsuarioIdAsync(usuario.Id);
                }
            }

            return usuario;
        }

        private async Task<List<Rol>> GetRolesByUsuarioIdAsync(int userId)
        {
            var roles = new List<Rol>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT r.Id, r.Nombre
                    FROM Roles r
                    INNER JOIN UsuarioRol ur ON r.Id = ur.fkIdRol
                    WHERE ur.fkIdUsuario = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            roles.Add(new Rol
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return roles;
        }
    }
}
