using Business_Logic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ScheduleReader : IScheduleRepository
    {
        private readonly string _connectionString;

        public ScheduleReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<VisitLogs>> GetSchedule(DateTime selectedDate)
        {
            List<VisitLogs> schedule = new List<VisitLogs>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("GetScheduleByDate", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Customer customer = new Customer
                            {
                                Id = Convert.ToInt32(reader["CustomerId"]),
                                CustomerFullName = reader["CustomerName"].ToString()
                            };

                            Service service = new Service
                            {
                                Id = Convert.ToInt32(reader["ServiceId"]),
                                ServiceName = reader["ServiceName"].ToString(),
                                ServicePrice = Convert.ToDecimal(reader["ServicePrice"])
                            };

                            Employee employee = new Employee
                            {
                                Id = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeFullName = reader["EmployeeName"].ToString()
                            };

                            VisitLogs item = new VisitLogs
                            {
                                Id = (int)reader["Id"],
                                Customer = customer,
                                Service = service,
                                Employee = employee,
                                StartDateTime = Convert.ToDateTime(reader["StartDateTime"]),
                                EndTime = (TimeSpan)reader["EndTime"],
                                Price = service.ServicePrice
                            };

                            schedule.Add(item);
                        }
                    }
                }

                schedule = schedule.OrderBy(s => s.StartDateTime).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return schedule;
        }

        public async Task<bool> DeleteAppointment(int appointmentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("DeleteAppointment", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
