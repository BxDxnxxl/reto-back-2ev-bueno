using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Videojuegos.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly string _connectionString;

        public RolRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Rol>> GetAllAsync()
        {
            var roles = new List<Rol>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre FROM Roles";

                using (var command = new SqlCommand(query, connection))
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
            return roles;
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            Rol? rol = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Nombre FROM Roles WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            rol = new Rol
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return rol;
        }

        public async Task AddAsync(Rol rol)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Roles (Nombre) VALUES (@Nombre)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Rol rol)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Roles SET Nombre = @Nombre WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", rol.Id);
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Roles WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task asignarRolesAUsuarios(UsuarioRolDto usuarioRolDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //quitamos los roles que tienen los usuarios para que no haya problemas a la hora de asignar los nuevos
                string deleteQuery = "DELETE FROM UsuarioRol WHERE fkIdUsuario = @fkIdUsuario";
                using (var deleteCommand = new SqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@fkIdUsuario", usuarioRolDto.UsuarioId);
                    await deleteCommand.ExecuteNonQueryAsync();
                }

                //otorgamos los nuevos roles
                foreach (var rolId in usuarioRolDto.RolesIds)
                {
                    string insertQuery = "INSERT INTO UsuarioRol (fkIdUsuario, fkIdRol) VALUES (@fkIdUsuario, @fkIdRol)";
                    using (var insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@fkIdUsuario", usuarioRolDto.UsuarioId);
                        insertCommand.Parameters.AddWithValue("@fkIdRol", rolId);
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                }
            }
        }

    }
}
