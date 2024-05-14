using Business_Logic;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace DataAccess
{
    public class ClientWriter
    {
        private readonly string _connectionsString;
        private readonly Customer _client;
        private readonly Service _service;
        private readonly Employee _employee;
        private readonly DateTime _appointmentDateTime;
        private readonly TimeSpan _executionTime;

        public bool RecordingIsSucsessfull = true;

        public ClientWriter(string connectionsString, Customer client, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan executionTime)
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
                        .Where(es => es.IdEmployee == _employee.Id)
                        .ToList();
                    bool isMasterBusy = employeeRecords.Any(es => es.DateTime <= _appointmentDateTime && es.EndTime >= _appointmentDateTime.TimeOfDay);


                    if (!await MasterIsAvailable(context))
                    {
                        return false;
                    }

                    TimeSpan _endTime = _appointmentDateTime.TimeOfDay + _executionTime;

                    Client_Service clientService = new Client_Service { IdCustomer = _client.Id, IdService = _service.Id, DateTime = _appointmentDateTime, EndTime = _endTime };
                    context.Client_Service.Add(clientService);

                    Employee_Service employeeService = new Employee_Service { IdEmployee = _employee.Id, IdService = _service.Id, DateTime = _appointmentDateTime, EndTime = _endTime };
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
            Customer existingCustomer = await context.Customers.FirstOrDefaultAsync(c => c.FullName == _client.FullName && c.BirthDate == _client.BirthDate);

            if (existingCustomer == null)
            {
                context.Customers.Add(_client);
                await context.SaveChangesAsync();
            }
            else
            {
                _client.Id = existingCustomer.Id;
            }
        }

        public async Task<bool> MasterIsAvailable(ClientRecordDbContext context)
        {
            DateTime appointmentDate = _appointmentDateTime.Date; 
            TimeSpan appointmentTime = _appointmentDateTime.TimeOfDay; 
            DateTime endTime = _appointmentDateTime.Add(_service.ExecutionTime); 
            TimeSpan executionTime = _service.ExecutionTime;

            var employeeRecords = await context.Employee_Service
                .Where(es => es.IdEmployee == _employee.Id &&
                es.EndTime >= appointmentTime &&
                es.DateTime <= endTime).ToListAsync();

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
