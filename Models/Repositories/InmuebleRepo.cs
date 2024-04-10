using System.Data;
using InmobiliariaOrtiz_Pascuali.Models;
using MySql.Data.MySqlClient;

public class InmuebleRepo
{
	readonly string connectionString;

	public InmuebleRepo()
	{
		connectionString = "Server=localhost;Database=inmobiliariaortiz_pascuali;Uid=root;Pwd=;";
	}

	public IList<Inmueble> ObtenerTodos()
	{
		IList<Inmueble> res = new List<Inmueble>();
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"SELECT i.IdInmueble, i.Direccion, i.Coordenadas, i.Cant_ambientes, i.Precio, i.IdPropietario,
                       p.Nombre, p.Apellido
                       FROM Inmueble i
                       INNER JOIN Propietario p ON i.IdPropietario = p.IdPropietario";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				connection.Open();
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					Inmueble inmueble = new Inmueble
					{
						IdInmueble = reader.GetInt32(0),
						Direccion = reader.GetString(1),
						Coordenadas = reader.GetString(2),
						CantAmbientes = reader.GetInt32(3),
						Precio = reader.GetInt32(4),
						IdPropietario = reader.GetInt32(5),
						propietario = new Propietario
						{
							IdPropietario = reader.GetInt32(5),
							Nombre = reader.GetString(6),
							Apellido = reader.GetString(7),
						}
					};
					res.Add(inmueble);
				}
				connection.Close();
			}
		}
		return res;
	}

	public int Insertar(Inmueble inmueble)
	{
		int res = -1;
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"INSERT INTO Inmueble 
					(Direccion, Coordenadas, Precio, IdPropietario)
					VALUES (@Direccion, @Coordenadas, @Precio, @IdPropietario);
					SELECT LAST_INSERT_ID();";//devuelve el id insertado 
			using (var command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
				command.Parameters.AddWithValue("@Coordenadas", inmueble.Coordenadas);
				command.Parameters.AddWithValue("@Precio", inmueble.Precio);
				command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
				connection.Open();
				res = Convert.ToInt32(command.ExecuteScalar());
				inmueble.IdInmueble = res;
				connection.Close();
			}
		}
		return res;
	}

	public int Eliminar(int id)
	{
		int res = -1;
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @$"DELETE FROM Inmueble WHERE IdInmueble = @id";
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

	public Inmueble BuscarInmueble(int id)
	{
		Inmueble entidad = null;
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"
    SELECT i.IdInmueble, i.Direccion, i.Coordenadas, i.Precio, i.IdPropietario, p.Nombre, p.Apellido
    FROM Inmueble i 
    JOIN Propietario p ON i.IdPropietario = p.IdPropietario
    WHERE i.IdInmueble = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				command.CommandType = CommandType.Text;
				connection.Open();
				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					entidad = new Inmueble
					{
						IdInmueble = reader.GetInt32(nameof(Inmueble.IdInmueble)),
						Direccion = reader.GetString("Direccion"),
						Coordenadas = reader.GetString("Coordenadas"),
						Precio = reader.GetInt32("Precio"),
						IdPropietario = reader.GetInt32("IdPropietario"),
						propietario = new Propietario
						{
							IdPropietario = reader.GetInt32("IdPropietario"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
						}
					};
				}
				connection.Close();
			}
		}
		return entidad;
	}

	public int ModificarInmueble(Inmueble entidad)
	{
		int res = -1;
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = "UPDATE Inmueble SET " +
"Direccion=@direccion, Coordenadas=@coordenadas, Precio=@precio, IdPropietario=@idPropietario " +
"WHERE IdInmueble = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@direccion", entidad.Direccion);
				command.Parameters.AddWithValue("@coordenadas", entidad.Coordenadas);
				command.Parameters.AddWithValue("@precio", entidad.Precio);
				command.Parameters.AddWithValue("@idPropietario", entidad.IdPropietario);
				command.Parameters.AddWithValue("@id", entidad.IdInmueble);
				command.CommandType = CommandType.Text;
				connection.Open();
				res = command.ExecuteNonQuery();
				connection.Close();
			}
		}
		return res;
	}




	public IList<Inmueble> BuscarPorDireccion(string direccion)
	{
		IList<Inmueble> res = new List<Inmueble>();
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"SELECT i.IdInmueble, i.Direccion, i.Coordenadas, i.Cant_ambientes, i.Precio, i.IdPropietario,
                   p.Nombre, p.Apellido
                   FROM Inmueble i
                   INNER JOIN Propietario p ON i.IdPropietario = p.IdPropietario
                   WHERE i.Direccion LIKE @direccion";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@direccion", $"%{direccion}%");
				command.CommandType = CommandType.Text;
				connection.Open();
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					Inmueble inmueble = new Inmueble
					{
						IdInmueble = reader.GetInt32(0),
						Direccion = reader.GetString(1),
						Coordenadas = reader.GetString(2),
						CantAmbientes = reader.GetInt32(3),
						Precio = reader.GetInt32(4),
						IdPropietario = reader.GetInt32(5),
						propietario = new Propietario
						{
							IdPropietario = reader.GetInt32(5),
							Nombre = reader.GetString(6),
							Apellido = reader.GetString(7),
						}
					};
					res.Add(inmueble);
				}
				connection.Close();
			}
		}
		return res;
	}




}