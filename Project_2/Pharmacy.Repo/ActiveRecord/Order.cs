using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pharmacy.ActiveRecord.ActiveRecord
{
    public class Order : ActiveRecord<Order>, IDisposable
    {
        protected SqlConnection _connection;
        public int Id { get; set; }
        public int? PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        //Virrtuals
        public Medicine Medicine { get; set; }
        public Prescription Prescription { get; set; }

        public override void Open()
        {
            string connectionString =
                "Integrated Security=SSPI;" + "Data Source=.\\SQLEXPRESS;" +
                "Initial Catalog=Apteka;";
            _connection = new SqlConnection();
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public override Order GetbyId(int id)
        {
            try
            {
                Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id], [PrescriptionId], [MedicineId], [Date], [Amount] " +
                        "FROM [Order] WHERE ID = @Id";
                    sqlcommand.Connection = _connection;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@Id";
                    sqlParameter.Value = id;
                    sqlParameter.DbType = DbType.Int32;
                    sqlcommand.Parameters.Add(sqlParameter);

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Order order = new Order();
                        order.Id = (int) sqlDataReader["Id"];
                        order.PrescriptionId = (int) sqlDataReader["PrescriptionId"];
                        order.MedicineId = (int) sqlDataReader["MedicineId"];
                        order.Date = (DateTime) sqlDataReader["Price"];
                        order.Amount = (decimal) sqlDataReader["Amount"];
                        return order;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dispose();
            }

            //return null;
            return this;
        }

        public override List<Order> Reload()
        {
            List<Order> list = new List<Order>();
            Open();
            try
            {
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id], [PrescriptionId], [MedicineId], [Date], [Amount]  FROM [Order] ";
                    sqlcommand.Connection = _connection;

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Order order = new Order()
                        {
                            Id = (int) sqlDataReader["Id"],
                            PrescriptionId = (int)sqlDataReader["PrescriptionId"],
                            MedicineId = (int)sqlDataReader["MedicineId"],
                            Date = (DateTime)sqlDataReader["Date"],
                            Amount = (decimal)sqlDataReader["Amount"],
                        };
                        list.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dispose();
            }

            return list;
        }

        public override int Save(Order objct)
        {
            try
            {
                Open();

                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    if (objct.Id == 0)
                    {
                        sqlcommand.CommandText =
                            "INSERT INTO [dbo].[Order] ([PrescriptionId], [MedicineId], [Date], [Amount]) " +
                            $"VALUES ('{objct.PrescriptionId}','{objct.MedicineId}','{objct.Date}','{objct.Amount}')";
                    }
                    else
                    {

                    }

                    sqlcommand.Connection = _connection;
                    sqlcommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dispose();
            }

            return 0; // do zmiany na id
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            _connection.Dispose();
        }
    }
}