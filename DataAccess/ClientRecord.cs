using Business_Logic;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace DataAccess
{
    public class ClientRecord
    {
        private string connectionsString;
        private Customer client;
        private Service service;
        private Employee employee;
        private DateTime appointmentDateTime;


        public bool RecordingIsSucsessfull = true;

        public ClientRecord(string connectionsString, Customer client, Service service, Employee employee, DateTime appointmentDateTime)
        {
            this.connectionsString = connectionsString;
            this.client = client;
            this.service = service;
            this.employee = employee;
            this.appointmentDateTime = appointmentDateTime;
        }

        public void ClientRecording()
        {
            try
            {
                using (ClientRecordDbContext context = new ClientRecordDbContext(connectionsString))
                {
                    CustomerIsExists(context);                    

                    if (MasterIsBusy(context))
                    {
                        return;
                    }

                    if (service != null && employee != null)
                    {
                        

                        Client_Service clientService = new Client_Service { idCustomer = client.id, idService = service.id, dateTime = appointmentDateTime };
                        context.Client_Service.Add(clientService);

                        Employee_Service employeeService = new Employee_Service { idEmployee = employee.id, idService = service.id, dateTime = appointmentDateTime };
                        context.Employee_Service.Add(employeeService);

                        context.SaveChanges();
                    }
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void CustomerIsExists(ClientRecordDbContext context)
        {
            Customer existingCustomer = context.Customers.FirstOrDefault(c => c.fullName == client.fullName && c.birthDate == client.birthDate);

            if (existingCustomer == null)
            {
                context.Customers.Add(client);
                existingCustomer = context.Customers.FirstOrDefault(c => c.fullName == client.fullName && c.birthDate == client.birthDate);
                context.SaveChanges();
            }
            else
            {
                client = existingCustomer;
            }
        }

        private bool MasterIsBusy(ClientRecordDbContext context)
        {
            var employeeRecords = context.Employee_Service
                        .Where(es => es.idEmployee == employee.id)
                        .ToList();

            bool isMasterBusy = employeeRecords.Any(es => es.dateTime.TimeOfDay <= appointmentDateTime.TimeOfDay && es.endTime >= appointmentDateTime.TimeOfDay);
            if (isMasterBusy) { RecordingIsSucsessfull = false; }

            return isMasterBusy;
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
