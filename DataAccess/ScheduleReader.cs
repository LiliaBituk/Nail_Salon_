using Business_Logic;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ScheduleReader
    {
        //private readonly ReadingDbContext _context;
        private readonly string _connectionString;

        public ScheduleReader(string connectionString)  //(DbContextOptions<ReadingDbContext> options, string connectionString)
        {
            //_context = new ReadingDbContext(options);
            _connectionString = connectionString;
        }

        public List<Schedule> GetSchedule(DateTime selectedDate)
        {
            List<Schedule> schedule = new List<Schedule>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            c.id AS CustomerId,
                            c.fullName AS ClientName,
                            s.id AS ServiceId,
                            s.name AS ServiceName,
                            cs.dateTime AS StartDateTime,
                            e.id AS EmployeeId,
                            e.fullName AS EmployeeName,
                            s.price AS ServicePrice
                        FROM 
                            Client_Service cs
                        JOIN 
                            Customers c ON cs.CustomerId = c.id
                        JOIN 
                            Services s ON cs.ServiceId = s.id
                        JOIN 
                            Employee_Service es ON cs.ServiceId = es.ServiceId AND cs.dateTime = es.dateTime
                        JOIN 
                            Employees e ON es.EmployeeId = e.id
                        WHERE 
                            CAST(cs.dateTime AS DATE) = @SelectedDate
                        ORDER BY 
                            cs.dateTime;";


                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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