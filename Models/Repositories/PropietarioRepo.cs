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

	public IList<Propietario> getPropietarios()
	{

		var propietarios = new List<Propietario>();

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Propietario.IdPropietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)} FROM Propietarios";

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
			string sql = $"INSERT INTO Propietarios ({nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)}) " +
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



}
