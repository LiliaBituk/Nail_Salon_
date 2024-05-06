using Business_Logic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ServiceReader
    {
        private string connectionString;

        public ServiceReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Service> GetAllServices()
        {
            List<Service> items = new List<Service>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT 
id AS id,
name AS serviceName,
price AS servicePrice,
type AS serviceType, 
executionTime AS executionTime
FROM Services";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string serviceName = reader.GetString(1);
                                decimal servicePrice = reader.GetDecimal(2);
                                string serviceType = reader.GetString(3);
                                TimeSpan executionTime = reader.GetTimeSpan(4);

                                Service item = new Service
                                {
                                    id = id,
                                    name = serviceName,
                                    type = serviceType,
                                    price = servicePrice,
                                    executionTime = executionTime
                                };
                                items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return items;
        }
    }
}
