using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pharmacy.ActiveRecord.ActiveRecord
{
    public class Medicine : ActiveRecord<Medicine>, IDisposable
    {
        protected SqlConnection _connection;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public bool WithPrescription { get; set; }

        public override void Open()
        {
            string connectionString =
                "Integrated Security=SSPI;" + "Data Source=.\\SQLEXPRESS;" +
                "Initial Catalog=Apteka;"; //.to local host 127.0.0.1
            _connection = new SqlConnection();
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public override Medicine GetbyId(int id)
        {
            try
            {
                Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id], [Name], [Manufacturer], [Price], [Amount], [WithPrescription] " +
                        "FROM [Medicine] WHERE ID = @Id";
                    sqlcommand.Connection = _connection;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@Id";
                    sqlParameter.Value = id;
                    sqlParameter.DbType = DbType.Int32;
                    sqlcommand.Parameters.Add(sqlParameter);

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Medicine medicine = new Medicine();
                        medicine.Id = (int) sqlDataReader["Id"];
                        medicine.Name = sqlDataReader["Name"].ToString();
                        medicine.Manufacturer = sqlDataReader["Manufacturer"].ToString();
                        medicine.Price = (decimal) sqlDataReader["Price"];
                        medicine.Amount = (decimal) sqlDataReader["Amount"];
                        medicine.WithPrescription = (bool) sqlDataReader["WithPrescription"];
                        return medicine;
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

            return this;
        }


        public override List<Medicine> Reload()
        {
            List<Medicine> list = new List<Medicine>();
            Open();
            try
            {
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText =
                        "SELECT [Id], [Name], [Manufacturer], [Price], [Amount], [WithPrescription] FROM [Medicine] ";
                    sqlcommand.Connection = _connection;

                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Medicine medicine = new Medicine()
                        {
                            Id = (int) sqlDataReader["Id"],
                            Name = sqlDataReader["Name"].ToString(),
                            Manufacturer = sqlDataReader["Manufacturer"].ToString(),
                            Price = (decimal) sqlDataReader["Price"],
                            Amount = (decimal) sqlDataReader["Amount"],
                            WithPrescription = (bool) sqlDataReader["WithPrescription"]
                        };
                        list.Add(medicine);
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

        public override int Save(Medicine objct)
        {
            try
            {
                Open();

                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    if (objct.Id == 0)
                    {
                        sqlcommand.CommandText =
                            "INSERT INTO [dbo].[Medicine] ([Name],[Manufacturer],[Price],[Amount],[WithPrescription]) " +
                            $"VALUES ('{objct.Name}','{objct.Manufacturer}','{objct.Price}','{objct.Amount}','{objct.WithPrescription}')";
                    }
                    else
                    {
                        //                        sqlcommand.CommandText =
                        //                            @"UPDATE [dbo].[Medicine]
                        //                              SET [Name] = '" + objct.Name + @"'
                        //                              ,[Manufacturer] = '" + objct.Manufacturer + @"'
                        //                              ,[Price] = '" + objct.Price + @"'
                        //                              ,[Amount] =  '" + objct.Amount + @"'
                        //                              ,[WithPrescription] = '" + objct.WithPrescription + @"'
                        //                              WHERE ID = @Id";               
                        sqlcommand.CommandText =
                            @"UPDATE [dbo].[Medicine] SET [Name] = '" + objct.Name + @"',[Manufacturer] = '" + objct.Manufacturer + @"' WHERE ID = @Id";
                            
                        SqlParameter sqlParameterId = new SqlParameter();
                        sqlParameterId.ParameterName = "@Id";
                        sqlParameterId.Value = objct.Id;
                        sqlParameterId.DbType = DbType.Int32;
                        sqlcommand.Parameters.Add(sqlParameterId);
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

            return 0;  // do zmiany na id
        }

        public override void Remove(int id)
        {
            try
            {
                Open();
                using (SqlCommand sqlcommand = new SqlCommand())
                {
                    sqlcommand.CommandText = "DELETE FROM [dbo].[Medicine] WHERE Id = @Id";

                    SqlParameter sqlParameterId = new SqlParameter();
                    sqlParameterId.ParameterName = "@Id";
                    sqlParameterId.Value = id;
                    sqlParameterId.DbType = DbType.Int32;
                    sqlcommand.Parameters.Add(sqlParameterId);

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
        }
        public override void Dispose()
        {
            _connection.Dispose();
        }
    }
}