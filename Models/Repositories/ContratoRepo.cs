using System.Data;
using InmobiliariaOrtiz_Pascuali.Models;
using MySql.Data.MySqlClient;

public class ContratoRepo
{
	readonly string connectionString;

	public ContratoRepo()
	{
		connectionString = "Server=localhost;Database=inmobiliariaortiz_pascuali;Uid=root;Pwd=;";
	}

	public IList<Contrato> ObtenerTodos()
	{
		IList<Contrato> res = new List<Contrato>();
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"SELECT c.IdContrato, c.fecha_inicio, c.fecha_fin, c.multa, c.estado, c.IdInmueble, c.IdInquilino,
                       i.Nombre, i.Apellido, i.Dni, inm.Direccion, inm.Coordenadas, inm.Precio
                       FROM Contrato c
                       INNER JOIN Inquilino i ON i.IdInquilino = c.IdInquilino 
					   INNER JOIN Inmueble inm ON inm.IdInmueble = c.IdInmueble";

			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				connection.Open();
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					Contrato contrato = new Contrato
					{
						IdContrato = reader.GetInt32(0),
						Fecha_inicio = reader.GetDateTime(1),
						Fecha_fin = reader.GetDateTime(2),
						Multa = reader.GetInt32(3),
						Estado = reader.GetBoolean(4),
						IdInmueble = reader.GetInt32(5),
						IdInquilino = reader.GetInt32(6),
						inquilino = new Inquilino
						{
							IdInquilino = reader.GetInt32(6),
							Nombre = reader.GetString(7),
							Apellido = reader.GetString(8),
							Dni = reader.GetString(9)
						},
						inmueble = new Inmueble
						{
							IdInmueble = reader.GetInt32(6),
							Direccion = reader.GetString(10),
							Coordenadas = reader.GetString(11),
							Precio = reader.GetDouble(12)
						}
					};
					res.Add(contrato);
				}
				connection.Close();
			}
		}
		return res;
	}

	public int CalcularPagos(DateTime fechaInicio, DateTime fechaFin)
	{
		int pagos = ((fechaFin.Year - fechaInicio.Year) * 12) + fechaFin.Month - fechaInicio.Month;
		return pagos;
	}

	public int Insertar(Contrato contrato)
	{
		int res = -1;
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"INSERT INTO Contrato 
					(Fecha_inicio, Fecha_fin, IdInmueble, IdInquilino)
					VALUES (@Fecha_inicio, @Fecha_fin, @IdInmueble, @IdInquilino);
					SELECT LAST_INSERT_ID();";//devuelve el id insertado 
			using (var command = new MySqlCommand(sql, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@Fecha_inicio", contrato.Fecha_inicio);
				command.Parameters.AddWithValue("@Fecha_fin", contrato.Fecha_fin);
				command.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
				command.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
				connection.Open();
				res = Convert.ToInt32(command.ExecuteScalar());
				contrato.IdContrato = res;
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
			string sql = @$"DELETE FROM Contrato WHERE IdContrato = @id";
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

	public Contrato BuscarContrato(int id)
	{


		Contrato entidad = null;
		using (var connection = new MySqlConnection(connectionString))
		{
			string sql = @"SELECT c.IdContrato, c.fecha_inicio, c.fecha_fin, c.multa, c.estado, c.IdInmueble, c.IdInquilino,
                       i.Nombre, i.Apellido, i.Dni, inm.Direccion, inm.Coordenadas, inm.Precio, inm.IdPropietario
                       FROM Contrato c
                       INNER JOIN Inquilino i ON i.IdInquilino = c.IdInquilino 
					   INNER JOIN Inmueble inm ON inm.IdInmueble = c.IdInmueble
                        WHERE c.IdContrato = @id";
			using (MySqlCommand command = new MySqlCommand(sql, connection))
			{
				command.Parameters.AddWithValue("@id", id);
				command.CommandType = CommandType.Text;
				connection.Open();
				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					entidad = new Contrato
					{
						IdContrato = reader.GetInt32(0),
						Fecha_inicio = reader.GetDateTime(1),
						Fecha_fin = reader.GetDateTime(2),
						Multa = reader.GetInt32(3),
						Estado = reader.GetBoolean(4),
						IdInmueble = reader.GetInt32(5),
						IdInquilino = reader.GetInt32(6),
						inquilino = new Inquilino
						{
							IdInquilino = reader.GetInt32(6),
							Nombre = reader.GetString(7),
							Apellido = reader.GetString(8),
							Dni = reader.GetString(9)
						},
						inmueble = new Inmueble
						{
							IdInmueble = reader.GetInt32(6),
							Direccion = reader.GetString(10),
							Coordenadas = reader.GetString(11),
							Precio = reader.GetDouble(12),
							IdPropietario = reader.GetInt32(13)
						}
					};
				}
				connection.Close();
			}
		}
		return entidad;
	}

		public int ModificarContrato(Contrato entidad)
		
		{
		
			int res = -1;
			using (var connection = new MySqlConnection(connectionString))
			{
				string sql = "UPDATE Contrato SET " +
	"Fecha_inicio=@Fecha_inicio, Fecha_fin=@Fecha_fin, IdInmueble=@IdInmueble, IdInquilino=@IdInquilino " +
	"WHERE IdContrato = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@Fecha_inicio", entidad.Fecha_inicio);
					command.Parameters.AddWithValue("@Fecha_fin", entidad.Fecha_fin);
					command.Parameters.AddWithValue("@IdInmueble", entidad.IdInmueble);
					command.Parameters.AddWithValue("@IdInquilino", entidad.IdInquilino);
					command.Parameters.AddWithValue("@id", entidad.IdContrato);
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}



}