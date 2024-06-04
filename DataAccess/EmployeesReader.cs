using Business_Logic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class EmployeesReader : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeesReader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Employee>> GetAllEmployeesAndCountScore()
        {
            List<Employee> employeesWithScore = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("GetAllEmployeesAndCountScore", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeFullName = reader["EmployeeFullName"].ToString(),
                                EmployeeTypeService = reader["EmployeeTypeService"].ToString(),
                                EmployeePhoneNumber = Convert.ToDecimal(reader["EmployeePhoneNumber"]),
                                Score = Convert.ToInt32(reader["Score"])
                            };

                            employeesWithScore.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return employeesWithScore;
        }

        public async Task<List<Employee>> GetEmployeesByServiceType(string serviceType)
        {
            List<Employee> employeesByServiceType = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand("GetEmployeesByServiceType", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ServiceType", serviceType);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Employee employee = new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EmployeeFullName = reader["FullName"].ToString(),
                                EmployeeTypeService = reader["TypeService"].ToString(),
                                EmploymentContractNumber = reader["EmploymentContractNumber"].ToString(),
                                EmployeeBirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                PermanentEmployeeStatus = Convert.ToBoolean(reader["PermanentEmployee"]),
                                EmployeePhoneNumber = Convert.ToDecimal(reader["PhoneNumber"])
                            };

                            employeesByServiceType.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return employeesByServiceType;
        }
    }
}
