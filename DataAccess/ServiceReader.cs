using Business_Logic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ServiceReader : IServiceRepository
    {
        private readonly string _connectionString;

        public ServiceReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Service>> GetAllServices()
        {
            List<Service> services = new List<Service>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GetAllServices", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Service service = new Service
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ServiceName = reader["Name"].ToString(),
                                    ServicePrice = Convert.ToDecimal(reader["Price"]),
                                    ServiceExecutionTime = (TimeSpan)reader["ExecutionTime"],
                                    ServiceType = reader["Type"].ToString()
                                };

                                services.Add(service);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return services;
        }
    }
}
