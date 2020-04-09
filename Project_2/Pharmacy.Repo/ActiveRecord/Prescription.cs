using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pharmacy.ActiveRecord.ActiveRecord
{
    public class Prescription : ActiveRecord<Prescription>, IDisposable
    {
        protected SqlConnection _connection;
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Pesel { get; set; }

        public string PrescriptionNumber { get; set; }
        public override void Open()
        {
            string connectionString =
                "Integrated Security=SSPI;" + "Data Source=.\\SQLEXPRESS;" +
                "Initial Catalog=Apteka;";
            _connection = new SqlConnection();
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public override Prescription GetbyId(int id)
        {
            try
            {
                Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id], [CustomerName], [Pesel], [PrescriptionNumber] " +
                        "FROM [Prescription] WHERE ID = @Id";
                    sqlcommand.Connection = _connection;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@Id";
                    sqlParameter.Value = id;
                    sqlParameter.DbType = DbType.Int32;
                    sqlcommand.Parameters.Add(sqlParameter);

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Prescription prescription = new Prescription();
                        prescription.Id = (int) sqlDataReader["Id"];
                        prescription.CustomerName = sqlDataReader["CustomerName"].ToString();
                        prescription.Pesel = sqlDataReader["Pesel"].ToString();
                        prescription.PrescriptionNumber = sqlDataReader["PrescriptionNumber"].ToString();

                        return prescription;
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

            return null;
        }

        public override List<Prescription> Reload()
        {
            List<Prescription> list = new List<Prescription>();
            Open();
            try
            {
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id], [CustomerName], [Pesel], [PrescriptionNumber] FROM [Prescription] ";
                    sqlcommand.Connection = _connection;

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Prescription prescription = new Prescription()
                        {
                            Id = (int) sqlDataReader["Id"],
                            CustomerName = sqlDataReader["CustomerName"].ToString(),
                            Pesel = sqlDataReader["Pesel"].ToString(),
                            PrescriptionNumber = sqlDataReader["PrescriptionNumber"].ToString()
                        };
                        list.Add(prescription);
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

        public override int Save(Prescription objct)
        {
            int result = 0;
            try
            {
                Open();

                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "INSERT INTO [Prescription] ([CustomerName], [PESEL], [PrescriptionNumber]) " +
                        $"VALUES ('{objct.CustomerName}','{objct.Pesel}','{objct.PrescriptionNumber}') ";
//                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
//                    while (sqlDataReader.Read())
//                    {
//                        result = Int32.Parse(sqlDataReader[0].ToString());
//                    }

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

            return result;
        }

        public List<Prescription> ShowId(Prescription objct)
        {
            List<Prescription> list = new List<Prescription>();
            try
            {
                Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id]" +
                        "FROM [Prescription] WHERE [CustomerName] = '" + objct.CustomerName + @"' ";


                    sqlcommand.Connection = _connection;

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Prescription prescription = new Prescription();
                        prescription.Id = (int) sqlDataReader["Id"];
                        list.Add(prescription);
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