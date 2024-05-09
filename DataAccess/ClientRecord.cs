using Business_Logic;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace DataAccess
{
    public class ClientRecord
    {
        private readonly string _connectionsString;
        private readonly Customer _client;
        private readonly Service _service;
        private readonly Employee _employee;
        private readonly DateTime _appointmentDateTime;
        private readonly TimeSpan _executionTime;

        public bool RecordingIsSucsessfull = true;

        public ClientRecord(string connectionsString, Customer client, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan executionTime)
        {
            _connectionsString = connectionsString;
            _client = client;
            _service = service;
            _employee = employee;
            _appointmentDateTime = appointmentDateTime;
            _executionTime = executionTime;
        }

        public async Task<bool> GetClientRecording()
        {
            try
            {
                using (ClientRecordDbContext context = new ClientRecordDbContext(_connectionsString))
                {
                    await CustomerIsExists(context);

                    var employeeRecords = context.Employee_Service
                        .Where(es => es.idEmployee == _employee.id)
                        .ToList();
                    bool isMasterBusy = employeeRecords.Any(es => es.dateTime <= _appointmentDateTime && es.endTime >= _appointmentDateTime.TimeOfDay);


                    if (!await MasterIsAvailable(context))
                    {
                        return false;
                    }

                    TimeSpan _endTime = _appointmentDateTime.TimeOfDay + _executionTime;

                    Client_Service clientService = new Client_Service { idCustomer = _client.id, idService = _service.id, dateTime = _appointmentDateTime, endTime = _endTime };
                    context.Client_Service.Add(clientService);

                    Employee_Service employeeService = new Employee_Service { idEmployee = _employee.id, idService = _service.id, dateTime = _appointmentDateTime, endTime = _endTime };
                    context.Employee_Service.Add(employeeService);

                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task CustomerIsExists(ClientRecordDbContext context)
        {
            Customer existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.fullName == _client.fullName && c.birthDate == _client.birthDate);

            if (existingCustomer == null)
            {
                context.Customers.Add(_client);
                await context.SaveChangesAsync();
            }
            else
            {
                _client.id = existingCustomer.id;
            }
        }

        public async Task<bool> MasterIsAvailable(ClientRecordDbContext context)
        {
            DateTime appointmentDate = _appointmentDateTime.Date; 
            TimeSpan appointmentTime = _appointmentDateTime.TimeOfDay; 
            DateTime endTime = _appointmentDateTime.Add(_service.executionTime); 
            TimeSpan executionTime = _service.executionTime;

            var employeeRecords = await context.Employee_Service
                .Where(es => es.idEmployee == _employee.id &&
                es.endTime >= appointmentTime &&
                es.dateTime <= endTime).ToListAsync();

            if (employeeRecords.Count > 0)
            {
                RecordingIsSucsessfull = false;
                return false;
            }
            else
            {
                RecordingIsSucsessfull = true;
                return true;
            }
        }
    }

    public class ClientRecordDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<Client_Service> Client_Service { get; set; }
        public DbSet<Employee_Service> Employee_Service { get; set; }

        public ClientRecordDbContext(string connectionString) : base(connectionString) { }
    }
}
