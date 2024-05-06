using Business_Logic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    public class EmployeesReader
    {

        private string connectionString;

        public EmployeesReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Employee> GetAllEmployeesAndCountScore()
        {
            List<Employee> items = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT 
    e.fullName AS fullName,
    e.typeService AS TypeService,
    e.phoneNumber AS PhoneNumber,
    (SELECT COUNT(DISTINCT CONCAT(es.idService, '-', es.[dateTime], '-', es.idEmployee, '-', es.endTime))
     FROM Employee_Service es
     WHERE es.idEmployee = e.id AND es.dateTime >= DATEADD(month, -2, GETDATE())) AS ProcedureCount
FROM 
    Employees e
ORDER BY 
    ProcedureCount DESC, e.fullName;

";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                string type = reader.GetString(1);
                                decimal phoneNumber = reader.GetDecimal(2);
                                int score = reader.GetInt32(3);

                                Employee item = new Employee
                                {
                                    fullName = name,
                                    typeService = type,
                                    phoneNumber = phoneNumber,
                                    score = score
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

        public List<Employee> GetEmployeesByServiceType(string serviceType)
        {
            List<Employee> items = new List<Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT id AS id,
fullName AS fullName, 
typeService AS typeService,
employmentContractNumber AS employmentContractNumber,
birthDate AS birthDate,
permanentEmployee AS permanentEmployee,
phoneNumber AS phoneNumber
FROM Employees
WHERE typeService = @TypeOfService
";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TypeOfService", serviceType);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string type = reader.GetString(2);
                                string employmentContractNumber = reader.GetString(3);
                                DateTime birthDate = reader.GetDateTime(4);
                                bool permanentEmployee = reader.GetBoolean(5);
                                decimal phoneNumber = reader.GetDecimal(6);

                                if (type == serviceType)
                                {
                                    Employee item = new Employee
                                    {
                                        id = id,
                                        fullName = name,
                                        typeService = type,
                                        birthDate = birthDate,
                                        permanentEmployee = permanentEmployee,
                                        employmentContractNumber = employmentContractNumber,
                                        phoneNumber = phoneNumber,
                                    };
                                    items.Add(item);
                                }
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
