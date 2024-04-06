namespace InmobiliariaOrtiz_Pascuali.Models;
using MySql.Data.MySqlClient;
using System.Data;

public class InquilinoRepo
{
    readonly string connectionString;

	public InquilinoRepo()
	{
		connectionString = "Server=localhost;Database=inmobiliariaortiz_pascuali;Uid=root;Pwd=;";
	}
	public Inquilino? buscarInquilino(int id)
	{

		Inquilino? inquilino = null;

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Inquilino.IdInquilino)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Dni)} 
			FROM Inquilino
			WHERE {nameof(Inquilino.IdInquilino)} = @{nameof(Inquilino.IdInquilino)}";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue($"@{nameof(Inquilino.IdInquilino)}", id);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						inquilino = new Inquilino
						{
							IdInquilino = reader.GetInt32(nameof(Inquilino.IdInquilino)),
							Nombre = reader.GetString(nameof(Inquilino.Nombre)),
							Apellido = reader.GetString(nameof(Inquilino.Apellido)),
							Dni = reader.GetString(nameof(Inquilino.Dni)),

						};


					}

				}

			}
		}
		return inquilino;
	}

	public IList<Inquilino> GetInquilinos()
	{

		var inquilinos = new List<Inquilino>();

		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @$"SELECT {nameof(Inquilino.IdInquilino)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Dni)} FROM Inquilino";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{

				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						inquilinos.Add(new Inquilino
						{
							IdInquilino = reader.GetInt32(nameof(Inquilino.IdInquilino)),
							Nombre = reader.GetString(nameof(Inquilino.Nombre)),
							Apellido = reader.GetString(nameof(Inquilino.Apellido)),
							Dni = reader.GetString(nameof(Inquilino.Dni)),

						});


					}

				}

			}
		}
		return inquilinos;
	}

	public int Insertar(Inquilino inquilino)
	{
		int id = 0;


		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = $"INSERT INTO Inquilino ({nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Dni)}) " +
						  $"VALUES (@Nombre, @Apellido, @Dni); SELECT LAST_INSERT_ID();";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{


				command.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}",inquilino.Apellido);
				command.Parameters.AddWithValue($"@{nameof(Inquilino.Dni)}", inquilino.Dni);
				connection.Open();
				id = Convert.ToInt32(command.ExecuteScalar());
				inquilino.IdInquilino = id;
				connection.Close();

			}
		}
		return id;
	}







	public int Modificar(Inquilino i)
	{
		int res = -1;
		using (MySqlConnection connection = new MySqlConnection(connectionString))
		{
			string sql = @"UPDATE Inquilino 
					SET Nombre=@nombre, Apellido=@apellido, Dni=@dni
					WHERE IdInquilino = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@nombre", i.Nombre);
				command.Parameters.AddWithValue("@apellido", i.Apellido);
				command.Parameters.AddWithValue("@dni", i.Dni);
				command.Parameters.AddWithValue("@id", i.IdInquilino);
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
			//por id hace la eliminacion
			string sql = @"DELETE FROM Inquilino WHERE IdInquilino = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}






}
