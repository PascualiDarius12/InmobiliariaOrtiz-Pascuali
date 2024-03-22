using System.ComponentModel.Design;
using MySql.Data.MySqlClient;

namespace InmobiliariaOrtiz_Pascuali.Models;


public class PropietarioRepo
{
    protected readonly string connectionString;

    public PropietarioRepo()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliariaortiz_pascuali;SslMode=none";
    }

   

    public List<Propietario> ObtenerPropietarios()
    {
        var propietario = new List<Propietario>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT idPropietario, nombre, apellido, dni, estado FROM propietarios";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propietario.Add(new Propietario
                        {
                            idPropietario = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido = reader.GetString(2),
                            dni = reader.GetString(3),
                            estado = reader.GetBoolean(4)

                        });
                    }
                }
                conn.Close();
            }
        }
        return propietario;
    }

public int Alta(Propietario p)
{
    int res = 0;
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        var sql = "INSERT INTO propietarios (nombre, apellido, dni, estado) VALUES (@nombre, @apellido, @dni, 1); SELECT LAST_INSERT_ID();";
        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
        {
            conn.Open();
            cmd.Parameters.AddWithValue("@nombre", p.nombre);
            cmd.Parameters.AddWithValue("@apellido", p.apellido);
            cmd.Parameters.AddWithValue("@dni", p.dni);
            cmd.Parameters.AddWithValue("@estado", p.estado);
            res = Convert.ToInt32(cmd.ExecuteScalar()); // cmd.ExecuteScalar();
            p.idPropietario = res;
            conn.Close();
        }
    }
    return res;
}




}
