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

        public async Task<List<Schedule>> GetSchedule(DateTime selectedDate)
        {
            List<Schedule> schedule = new List<Schedule>();

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
                                CustomerFullName = reader["ClientName"].ToString()
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

                            Schedule item = new Schedule
                            {
                                Customer = customer,
                                Service = service,
                                Employee = employee,
                                StartDateTime = Convert.ToDateTime(reader["StartDateTime"]),
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
    }
}
