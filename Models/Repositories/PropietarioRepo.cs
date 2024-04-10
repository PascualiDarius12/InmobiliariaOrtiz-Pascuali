using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace InmobiliariaOrtiz_Pascuali.Models;

public class PropietarioRepo
{

	readonly string connectionString;

	public PropietarioRepo()
	{
		connectionString = "Server=localhost;Database=inmobiliariaortiz_pascuali;Uid=root;Pwd=;";
	}
	public Propietario? buscarPropietario(int id)
	{

		Propietario? propietario = null;

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Propietario.IdPropietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)} 
			FROM Propietario
			WHERE {nameof(Propietario.IdPropietario)} = @{nameof(Propietario.IdPropietario)}";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Propietario.IdPropietario)}", id);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						propietario = new Propietario
						{
							IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
							Nombre = reader.GetString(nameof(Propietario.Nombre)),
							Apellido = reader.GetString(nameof(Propietario.Apellido)),
							Dni = reader.GetString(nameof(Propietario.Dni)),

						};


					}

				}

			}
		}
		return propietario;
	}

	public IList<Propietario> getPropietarios()
	{

		var propietarios = new List<Propietario>();

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Propietario.IdPropietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)} FROM Propietario";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{

				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						propietarios.Add(new Propietario
						{
							IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
							Nombre = reader.GetString(nameof(Propietario.Nombre)),
							Apellido = reader.GetString(nameof(Propietario.Apellido)),
							Dni = reader.GetString(nameof(Propietario.Dni)),

						});


					}

				}

			}
		}
		return propietarios;
	}

	public int Insertar(Propietario propietario)
	{
		int id = 0;


		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = $"INSERT INTO Propietario ({nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)}) " +
						  $"VALUES (@Nombre, @Apellido, @Dni); SELECT LAST_INSERT_ID();";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{


				command.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
				command.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
				command.Parameters.AddWithValue($"@{nameof(Propietario.Dni)}", propietario.Dni);

				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				propietario.IdPropietario = id;
				connection.Close();

			}
		}
		return id;
	}







	public int Modificar(Propietario p)
	{
		int res = -1;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @"UPDATE Propietario 
					SET Nombre=@nombre, Apellido=@apellido, Dni=@dni
					WHERE IdPropietario = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@nombre", p.Nombre);
				command.Parameters.AddWithValue("@apellido", p.Apellido);
				command.Parameters.AddWithValue("@dni", p.Dni);
				command.Parameters.AddWithValue("@id", p.IdPropietario);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}


	public int Eliminar(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = "DELETE FROM Propietario WHERE IdPropietario = @id";
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



		public IList<Propietario> BuscarPorDNI(string dni)
{
    var propietarios = new List<Propietario>();

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        string sql = @$"SELECT {nameof(Propietario.IdPropietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)} 
                        FROM Propietario
                        WHERE {nameof(Propietario.Dni)} LIKE @dni";

        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@dni", $"%{dni}%");
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    propietarios.Add(new Propietario
                    {
                        IdPropietario = reader.GetInt32(nameof(Propietario.IdPropietario)),
                        Nombre = reader.GetString(nameof(Propietario.Nombre)),
                        Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        Dni = reader.GetString(nameof(Propietario.Dni))
                    });
                }
            }
        }
    }
    return propietarios;
}




}
