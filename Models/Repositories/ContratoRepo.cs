using System.Data;
using InmobiliariaOrtiz_Pascuali.Models;
using MySql.Data.MySqlClient;

namespace InmobiliariaOrtiz_Pascuali.Models;
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




    public int Insertar(Contrato contrato)
    {
        int res = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"INSERT INTO Contrato (Fecha_inicio, Fecha_fin, idInquilino, idInmueble)
    VALUES (@Fecha_inicio, @Fecha_fin, @idInquilino, @idInmueble);SELECT LAST_INSERT_ID();";

        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@Fecha_inicio", contrato.Fecha_inicio);
            command.Parameters.AddWithValue("@Fecha_fin", contrato.Fecha_fin);
            command.Parameters.AddWithValue("@idInquilino", contrato.idInquilino);
            command.Parameters.AddWithValue("@idInmueble", contrato.idInmueble);
            
            connection.Open();
            res = Convert.ToInt32(command.ExecuteScalar());
            contrato.idContrato = res;
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
            string sql = @$"DELETE FROM Contrato WHERE idContrato = @id";
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

/*
    public Contrato BuscarContrato(int id)
    {
        Contrato entidad = null;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT i.IdContrato, i.Direccion, i.Coordenadas, i.Precio, i.IdPropietario, p.Nombre, p.Apellido
            FROM Contrato i 
            JOIN Propietario p ON i.IdPropietario = p.IdPropietario
            WHERE i.IdContrato = @id";
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
*/

public Contrato BuscarContrato(int id)
{
    Contrato entidad = null;
    using (var connection = new MySqlConnection(connectionString))
    {
        string sql = @"
            SELECT 
                c.idContrato, c.Fecha_inicio, c.Fecha_fin, c.idInquilino, i.Nombre AS NombreInquilino, i.Apellido AS ApellidoInquilino, 
                c.idInmueble, m.Direccion AS DireccionInmueble, m.Precio AS PrecioInmueble
            FROM 
                Contrato c
                INNER JOIN Inquilino i ON c.idInquilino = i.IdInquilino
                INNER JOIN Inmueble m ON c.idInmueble = m.IdInmueble
            WHERE c.idContrato = @id";

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
                    idContrato = reader.GetInt32("idContrato"),
                    Fecha_inicio = reader.GetDateTime("Fecha_inicio"),
                    Fecha_fin = reader.GetDateTime("Fecha_fin"),
                    idInquilino = reader.GetInt32("idInquilino"),
                    objetoInquilino = new Inquilino
                    {
                        idInquilino = reader.GetInt32("idInquilino"),
                        Nombre = reader.GetString("NombreInquilino"),
                        Apellido = reader.GetString("ApellidoInquilino")
                    },
                    idInmueble = reader.GetInt32("idInmueble"),
                    objetoInmueble = new Inmueble
                    {
                        idInmueble = reader.GetInt32("idInmueble"),
                        Direccion = reader.GetString("DireccionInmueble"),
                        Precio = reader.GetInt32("PrecioInmueble")
                    }
                };
            }
            connection.Close();
        }
    }
    return entidad;
}



    public int ModificarContrato(Contrato contrato)
    {
        int res = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "UPDATE Contrato SET " +
                "idContrato=@idContrato, Fecha_inicio=@Fecha_inicio, Fecha_fin=@Fecha_fin, idInquilino=@idInquilino, idInmueble=@idInmueble" +
                "WHERE idContrato = @id";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", contrato.idContrato);
                command.Parameters.AddWithValue("@Fecha_inicio", contrato.Fecha_inicio);
                command.Parameters.AddWithValue("@Fecha_fin", contrato.Fecha_fin);
                command.Parameters.AddWithValue("@idInquilino", contrato.idInquilino);
                command.Parameters.AddWithValue("@idInmueble", contrato.idInmueble);
                command.CommandType = CommandType.Text;
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }



}