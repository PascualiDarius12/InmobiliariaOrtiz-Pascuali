using System;
using System.Collections.Generic;
using System.Data;
using InmobiliariaOrtiz_Pascuali.Models;
using MySql.Data.MySqlClient;

namespace InmobiliariaOrtiz_Pascuali.Models;
public class PagoRepo
{
    readonly string connectionString;

    public PagoRepo()
    {
        connectionString = "Server=localhost;Database=inmobiliariaortiz_pascuali;Uid=root;Pwd=;";
    }

                    //trabajandolo

    public IList<Pago> ObtenerPagos(int id)
    {

        IList<Pago> pagos = new List<Pago>();
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = @"SELECT IdPago, Fecha_pago, Monto
                      FROM Pago
                      WHERE IdContrato = @IdContrato";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IdContrato", id); // Verificar aquí
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pago pago = new Pago
                    {
                        IdPago = reader.GetInt32(0),
                        Fecha_pago = reader.GetDateTime(1),
                        Monto = reader.GetInt32(2),
                        IdContrato = id,
                    };
                    pagos.Add(pago);
                }
            }
            return pagos;
        }
    }

    public int CalcularPagos(DateTime fechaInicio, DateTime fechaFin)
    {
        int pagos = ((fechaFin.Year - fechaInicio.Year) * 12) + fechaFin.Month - fechaInicio.Month;
        return pagos;
    }







/*

    public IList<Pago> ObtenerTodos()
    {
        IList<Pago> pagos = new List<Pago>();
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "SELECT IdPago, Fecha_pago, Monto, IdContrato, Estado FROM Pago";

            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Pago pago = new Pago
                    {
                        IdPago = reader.GetInt32(0),
                        Fecha_pago = reader.GetDateTime(1),
                        Monto = reader.GetDouble(2),
                        IdContrato = reader.GetInt32(3),
                        Estado = reader.GetBoolean(4)
                    };

                    pagos.Add(pago);
                }
                connection.Close();
            }
        }
        return pagos;
    }

    public int Insertar(Pago pago)
    {
        int pagoId = -1;
        using (var connection = new MySqlConnection(connectionString))
        {
            string sql = "INSERT INTO Pago (Fecha_pago, Monto, IdContrato, Estado) " +
                         "VALUES (@Fecha_pago, @Monto, @IdContrato, @Estado); " +
                         "SELECT LAST_INSERT_ID();";

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Fecha_pago", pago.Fecha_pago);
                command.Parameters.AddWithValue("@Monto", pago.Monto);
                command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                command.Parameters.AddWithValue("@Estado", pago.Estado);

                try
                {
                    connection.Open();
                    pagoId = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según sea necesario
                    Console.WriteLine("Error al insertar el pago: " + ex.Message);
                }
            }
        }
        return pagoId;
    }

*/



}
