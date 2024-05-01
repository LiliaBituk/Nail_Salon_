////using System.Collections.Generic;
////using System;
////using Business_Logic;
////using Microsoft.Data.SqlClient;

////namespace DataAccess
////{
////    public class ScheduleReader
////    {
////        private string connectionString;

////        public ScheduleReader(string connectionString)
////        {
////            this.connectionString = connectionString;
////        }

////        public List<Schedule> getData(DateTime selectedDate)
////        {
////            List<Schedule> items = new List<Schedule>();

////            try
////            {
////                using (SqlConnection connection = new SqlConnection(connectionString))
////                {
////                    connection.Open();
////                    string sqlQuery = @"SELECT DISTINCT 
////                                            c.fullName AS CustomerName, 
////                                            s.name AS ServiceName, 
////                                            cs.dataTime AS DataTime, 
////                                            e.fullName AS EmployeeName, 
////                                            s.price AS Price 
////                                        FROM 
////                                            Client_Service cs 
////                                            JOIN Customers c ON cs.idCustomer = c.id 
////                                            JOIN Services s ON cs.idService = s.id 
////                                            JOIN Employee_Service es ON es.idService = s.id 
////                                            JOIN Employees e ON es.idEmployee = e.id 
////                                        WHERE 
////                                            CONVERT(date, cs.dataTime) = @SelectedDate 
////                                        ORDER BY 
////                                            cs.dataTime";

////                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
////                    {
////                        command.Parameters.AddWithValue("@SelectedDate", selectedDate);
////                        using (SqlDataReader reader = command.ExecuteReader())
////                        {
////                            while (reader.Read())
////                            {
////                                string customerName = reader.GetString(0);
////                                string serviceName = reader.GetString(1);
////                                DateTime dataTime = reader.GetDateTime(2);
////                                string employeeName = reader.GetString(3);
////                                decimal price = reader.GetDecimal(4);

////                                Schedule item = new Schedule
////                                {
////                                    CustomerName = customerName,
////                                    ServiceName = serviceName,
////                                    StartDateTime = dataTime,
////                                    EmployeeName = employeeName,
////                                    Price = price
////                                };
////                                items.Add(item);
////                            }
////                        }
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                Console.WriteLine(ex.Message);
////            }

////            return items;
////        }
////    }
////}

//using Business_Logic;
//using Microsoft.Data.SqlClient;

//namespace DataAccess
//{
//    public class ScheduleReader
//    {
//        private string connectionString;

//        public ScheduleReader(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }

//        public List<Schedule> getData(DateTime selectedDate)
//        {
//            List<Schedule> items = new List<Schedule>();

//            try
//            {
//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    connection.Open();
//                    string sqlQuery = @"SELECT DISTINCT 
//                                            c.fullName AS CustomerName, 
//                                            s.name AS ServiceName, 
//                                            cs.dataTime AS DataTime, 
//                                            e.fullName AS EmployeeName, 
//                                            s.price AS Price 
//                                        FROM 
//                                            Client_Service cs 
//                                            JOIN Customers c ON cs.idCustomer = c.id 
//                                            JOIN Services s ON cs.idService = s.id 
//                                            JOIN Employee_Service es ON es.idService = s.id 
//                                            JOIN Employees e ON es.idEmployee = e.id 
//                                        WHERE 
//                                            CONVERT(date, cs.dataTime) = @SelectedDate 
//                                        ORDER BY 
//                                            cs.dataTime";

//                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
//                    {
//                        command.Parameters.AddWithValue("@SelectedDate", selectedDate);
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                string customerName = reader.GetString(0);
//                                string serviceName = reader.GetString(1);
//                                DateTime dataTime = reader.GetDateTime(2);
//                                string employeeName = reader.GetString(3);
//                                decimal price = reader.GetDecimal(4);

//                                Schedule item = new Schedule
//                                {
//                                    CustomerName = customerName,
//                                    ServiceName = serviceName,
//                                    StartDateTime = dataTime,
//                                    EmployeeName = employeeName,
//                                    Price = price
//                                };
//                                items.Add(item);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }

//            return items;
//        }
//    }
//}

using System.Collections.Generic;
using System;
using Business_Logic;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class ScheduleReader
    {
        private string connectionString;

        public ScheduleReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Schedule> getData(DateTime selectedDate)
        {
            List<Schedule> items = new List<Schedule>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = @"SELECT DISTINCT 
                                            c.fullName AS CustomerName, 
                                            s.name AS ServiceName, 
                                            cs.dataTime AS DataTime, 
                                            e.fullName AS EmployeeName, 
                                            s.price AS Price 
                                        FROM 
                                            Client_Service cs 
                                            JOIN Customers c ON cs.idCustomer = c.id 
                                            JOIN Services s ON cs.idService = s.id 
                                            JOIN Employee_Service es ON es.idService = s.id 
                                            JOIN Employees e ON es.idEmployee = e.id 
                                        WHERE 
                                            CONVERT(date, cs.dataTime) = @SelectedDate 
                                        ORDER BY 
                                            cs.dataTime";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedDate", selectedDate);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string customerName = reader.GetString(0);
                                string serviceName = reader.GetString(1);
                                DateTime dataTime = reader.GetDateTime(2);
                                string employeeName = reader.GetString(3);
                                decimal price = reader.GetDecimal(4);

                                Schedule item = new Schedule
                                {
                                    CustomerName = customerName,
                                    ServiceName = serviceName,
                                    StartDateTime = dataTime,
                                    EmployeeName = employeeName,
                                    Price = price
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