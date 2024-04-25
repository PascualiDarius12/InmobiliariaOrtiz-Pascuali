using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace InmobiliariaOrtiz_Pascuali.Models;

public class UsuarioRepo
{

	readonly string connectionString;

	public UsuarioRepo()
	{
		connectionString = "Server=localhost;Database=inmobiliariaortiz_pascuali;Uid=root;Pwd=;";
	}


	public IList<Usuario> getUsuarios()
	{

		var usuarios = new List<Usuario>();

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Usuario.IdUsuario)}, {nameof(Usuario.Nombre)}, {nameof(Usuario.Apellido)}, {nameof(Usuario.Email)}, {nameof(Usuario.Rol)}, {nameof(Usuario.Avatar)} FROM Usuario";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{

				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						usuarios.Add(new Usuario
						{
							IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
							Nombre = reader.GetString(nameof(Usuario.Nombre)),
							Apellido = reader.GetString(nameof(Usuario.Apellido)),
							Email = reader.GetString(nameof(Usuario.Email)),
							Rol = reader.GetInt32(nameof(Usuario.Rol)),
							Avatar = reader.GetString(nameof(Usuario.Avatar)),

						});


					}

				}

			}
		}
		return usuarios;
	}

	public int Crear(Usuario usuario)
	{
		int id = 0;


		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = $"INSERT INTO Usuario ({nameof(Usuario.Nombre)}, {nameof(Usuario.Apellido)}, {nameof(Usuario.Email)}, {nameof(Usuario.Clave)}, {nameof(Usuario.Rol)}) " +
						  $"VALUES (@Nombre, @Apellido, @Email, @Clave, @Rol); SELECT LAST_INSERT_ID();";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{


				command.Parameters.AddWithValue($"@{nameof(Usuario.Nombre)}", usuario.Nombre);
				command.Parameters.AddWithValue($"@{nameof(Usuario.Apellido)}", usuario.Apellido);
				command.Parameters.AddWithValue($"@{nameof(Usuario.Email)}", usuario.Email);
				command.Parameters.AddWithValue($"@{nameof(Usuario.Clave)}", usuario.Clave);
				command.Parameters.AddWithValue($"@{nameof(Usuario.Rol)}", usuario.Rol);

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				usuario.IdUsuario = id;
				connection.Close();

			}
		}
		return id;
	}

	// public int Crear(Usuario e)
	// 	{
	// 		int res = -1;
	// 		using (MySqlConnection connection = new MySqlConnection(connectionString))
	// 		{
	// 			string sql = @"INSERT INTO Usuario
	// 				(Nombre, Apellido, Avatar, Email, Clave, Rol) 
	// 				VALUES (@nombre, @apellido, @email, @clave, @rol);
	// 				SELECT LAST_INSERT_ID();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
	// 			using (MySqlCommand command = new MySqlCommand(sql, connection))
	// 			{
	// 				command.CommandType = CommandType.Text;
	// 				command.Parameters.AddWithValue("@nombre", e.Nombre);
	// 				command.Parameters.AddWithValue("@apellido", e.Apellido);
	// 				// if (String.IsNullOrEmpty(e.Avatar))
	// 				// 	command.Parameters.AddWithValue("@avatar", DBNull.Value);
	// 				// else
	// 				// 	command.Parameters.AddWithValue("@avatar", e.Avatar);
	// 				command.Parameters.AddWithValue("@email", e.Email);
	// 				command.Parameters.AddWithValue("@clave", e.Clave);
	// 				command.Parameters.AddWithValue("@rol", e.Rol);
	// 				connection.Open();
	// 				res = Convert.ToInt32(command.ExecuteScalar());
	// 				e.IdUsuario = res;
	// 				connection.Close();
	// 			}
	// 		}
	// 		return res;
	// 	}

	public Usuario crearClave(Usuario usuario, IConfiguration configuration)
	{
		string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							   password: usuario.Clave,
							   salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
							   prf: KeyDerivationPrf.HMACSHA1,
							   iterationCount: 1000,
							   numBytesRequested: 256 / 8));
		usuario.Clave = hashed;
		return usuario;
	}

	public int Modificacion(Usuario e)
	{
		int res = -1;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @"UPDATE Usuario
					SET Nombre=@nombre, Apellido=@apellido, Email=@email, Clave=@clave, Rol=@rol
					WHERE IdUsuario = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@nombre", e.Nombre);
				command.Parameters.AddWithValue("@apellido", e.Apellido);

				command.Parameters.AddWithValue("@email", e.Email);
				command.Parameters.AddWithValue("@clave", e.Clave);
				command.Parameters.AddWithValue("@rol", e.Rol);
				command.Parameters.AddWithValue("@id", e.IdUsuario);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}


	public Usuario ObtenerPorEmail(string email)
	{
		Usuario? e = null;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @"SELECT
					IdUsuario, Nombre, Apellido, Avatar, Email, Clave, Rol FROM Usuario
					WHERE Email=@email";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
				connection.Open();
				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					e = new Usuario
					{
						IdUsuario = reader.GetInt32("IdUsuario"),
						Nombre = reader.GetString("Nombre"),
						Apellido = reader.GetString("Apellido"),
						Avatar = reader.GetString("Avatar"),
						Email = reader.GetString("Email"),
						Clave = reader.GetString("Clave"),
						Rol = reader.GetInt32("Rol"),
					};
				}
				connection.Close();
			}
		}
		return e;
	}


public int ModificarAvatar(int idUsuario, string nuevoAvatar)
{
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        string sql = @"UPDATE Usuario
                       SET Avatar = @avatar
                       WHERE IdUsuario = @id";
        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@avatar", nuevoAvatar);
            command.Parameters.AddWithValue("@id", idUsuario);
            connection.Open();
            res = command.ExecuteNonQuery();
            connection.Close();
        }
    }
    return res;
}

public int EliminarAvatar(int idUsuario)
{
    int res = -1;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        string sql = @"UPDATE Usuario
                       SET Avatar = ''
                       WHERE IdUsuario = @id";
        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@id", idUsuario);
            connection.Open();
            res = command.ExecuteNonQuery();
            connection.Close();
        }
    }
    return res;
}


	public Usuario ObtenerPorId(int id)
	{
		Usuario usuario = null;

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @"SELECT IdUsuario, Nombre, Apellido, Email, Rol, Avatar, Clave FROM Usuario WHERE IdUsuario = @IdUsuario";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@IdUsuario", id);

				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						usuario = new Usuario
						{
							IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)), //reader.GetInt32("IdUsuario"),
							Nombre = reader.GetString(nameof(Usuario.Nombre)),
							Apellido = reader.GetString(nameof(Usuario.Apellido)),
							Email = reader.GetString(nameof(Usuario.Email)),
							Rol = reader.GetInt32(nameof(Usuario.Rol)),
							Avatar = reader.GetString(nameof(Usuario.Avatar)),
							Clave = reader.GetString(nameof(Usuario.Clave))
						};
					}
				}
			}
		}


		return usuario;
	}




	public int Eliminar(int id)
	{
		int res = -1;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = "DELETE FROM Usuario WHERE IdUsuario = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@id", id);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}




	//buscador por nombre
	public IList<Usuario> BuscarPorNombre(string nombre)
	{
		var usuarios = new List<Usuario>();

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Usuario.IdUsuario)}, {nameof(Usuario.Nombre)}, {nameof(Usuario.Apellido)}, {nameof(Usuario.Email)}, {nameof(Usuario.Rol)} 
                        FROM Usuario WHERE {nameof(Usuario.Nombre)} LIKE @nombre";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@nombre", $"%{nombre}%");
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						usuarios.Add(new Usuario
						{
							IdUsuario = reader.GetInt32(nameof(Usuario.IdUsuario)),
							Nombre = reader.GetString(nameof(Usuario.Nombre)),
							Apellido = reader.GetString(nameof(Usuario.Apellido)),
							Email = reader.GetString(nameof(Usuario.Email)),
							Rol = reader.GetInt32(nameof(Usuario.Rol))
						});
					}
				}
			}
		}
		return usuarios;
	}



}
