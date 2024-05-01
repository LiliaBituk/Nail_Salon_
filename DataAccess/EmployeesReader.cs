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

        public List<Employees> getData()
        {
            List<Employees> items = new List<Employees>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT 
    e.fullName AS EmployeeName,
    e.typeService AS ServiceType,
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

                                Employees item = new Employees
                                {
                                    EmployeeName = name,
                                    ServiceType = type,
                                    PhoneNumber = phoneNumber,
                                    Score = score
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
