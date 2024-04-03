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
    using (var connection = new MySqlConnection(connectionString)){

        string sql = @$"SELECT {nameof(Contrato.idContrato)}, {nameof(Contrato.Fecha_inicio)}, {nameof(Contrato.Fecha_fin)}, {nameof(Contrato.idInquilino)}, {nameof(Contrato.idInmueble)}
                        FROM Contrato";
        using (MySqlCommand command = new MySqlCommand(sql, connection)){

            command.CommandType = CommandType.Text;
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Contrato contrato = new Contrato
                {
                    idContrato = reader.GetInt32(0),
                    Fecha_inicio = reader.GetDateTime(1),
                    Fecha_fin = reader.GetDateTime(2),
                    idInquilino = reader.GetInt32(3),
                    idInmueble = reader.GetInt32(4)
                };
                res.Add(contrato);
            }
            connection.Close();
        }
    }
    return res;
}



/*
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


*/
}