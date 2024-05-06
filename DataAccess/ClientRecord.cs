using Business_Logic;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ClientRecord
    {
        private string connectionsString;
        private Customer client;
        private Service service;
        private Employee employee;
       // private int employeeId;

        public ClientRecord(string connectionsString, Customer client, Service service, Employee employee)
        {
            this.connectionsString = connectionsString;
            this.client = client;
            this.service = service;
            this.employee = employee;
            //this.employeeId = employeeId;
        }

        public void ClientRecording()
        {
            try
            {
                using (ClientRecordDbContext context = new ClientRecordDbContext(connectionsString))
                {
                    Customer existingCustomer = context.Customers.FirstOrDefault(c => c.fullName == client.fullName && c.birthDate == client.birthDate);

                    if (existingCustomer == null)
                    {
                        // Клиент не существует, добавляем его в базу данных
                        context.Customers.Add(client);
                        context.SaveChanges(); // Сохраняем изменения, чтобы получить идентификатор клиента
                    }
                    else
                    {
                        // Клиент уже существует, используем его данные
                        client.id = existingCustomer.id;
                    }

                    DateTime appointmentDateTime = new DateTime(2024, 4, 29, 18, 30, 0);
                    TimeSpan appointmentTime = appointmentDateTime.TimeOfDay;

                    // Получаем записи мастера из базы данных
                    var employeeRecords = context.Employee_Service
                        .Where(es => es.idEmployee == employee.id)
                        .ToList();


                    // Проверяем, есть ли записи мастера, которые перекрывают время выбранной записи клиента
                    bool isMasterBusy = employeeRecords.Any(es => es.dateTime.TimeOfDay <= appointmentTime && es.endTime.TimeOfDay >= appointmentTime);

                    if (isMasterBusy)
                    {
                        // Мастер уже занят в это время
                        // Возможно, вы захотите отобразить сообщение об ошибке
                        return; // Завершаем операцию
                    }

                    if (service != null && employee != null)
                    {
                        // Создаем запись клиента
                        Client_Service clientService = new Client_Service { idCustomer = client.id, idService = service.id, dateTime = appointmentDateTime };
                        context.Client_Service.Add(clientService);

                        // Создаем запись мастера
                        Employee_Service employeeService = new Employee_Service { idEmployee = employee.id, idService = service.id, dateTime = appointmentDateTime };
                        context.Employee_Service.Add(employeeService);

                        context.SaveChanges();
                    }
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                // Выводим сообщение об ошибке EntityCommandExecutionException
                Console.WriteLine("Произошла ошибка при выполнении команды в базе данных:");
                Console.WriteLine(ex.Message);
            }

        }

    }

    internal class ClientRecordDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Client_Service> Client_Service { get; set; }
        public DbSet<Employee_Service> Employee_Service { get; set; }

        public ClientRecordDbContext(string connectionString) : base(connectionString) { }
    }
}
