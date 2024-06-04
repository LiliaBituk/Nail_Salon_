using Business_Logic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerWriter : ICustomerRepository
    {
        private readonly string _connectionsString;

        public CustomerWriter(string connectionsString)
        {
            _connectionsString = connectionsString;
        }

        public async Task RecordCustomerAsync(Customer customer, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan endTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionsString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("RecordCustomer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CustomerId", customer.Id);
                        command.Parameters.AddWithValue("@ServiceId", service.Id);
                        command.Parameters.AddWithValue("@EmployeeId", employee.Id);
                        command.Parameters.AddWithValue("@AppointmentDateTime", appointmentDateTime);
                        command.Parameters.AddWithValue("@ServiceEndTime", endTime);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public bool AddOrUpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionsString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddOrUpdateCustomer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName);
                        command.Parameters.AddWithValue("@CustomerBirthDate", customer.CustomerBirthDate);
                        command.Parameters.AddWithValue("@CustomerPhoneNumber", customer.CustomerPhoneNumber);

                        var customerIsNewParam = new SqlParameter("@CustomerIsNew", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(customerIsNewParam);

                        var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(customerIdParam);

                        command.ExecuteNonQuery();

                        customer.CustomerIsNew = (bool)customerIsNewParam.Value;
                        if (!customer.CustomerIsNew)
                        {
                            customer.Id = (int)customerIdParam.Value;
                        }
                    }

                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool IsEmployeeAvailable(int employeeId, DateTime appointmentDateTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionsString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("IsEmployeeAvailable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@EmployeeId", employeeId);
                        command.Parameters.AddWithValue("@AppointmentDateTime", appointmentDateTime);

                        var isAvailableParam = new SqlParameter("@IsAvailable", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(isAvailableParam);

                        command.ExecuteNonQuery();

                        return (bool)isAvailableParam.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
