using Business_Logic;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace DataAccess
{
    public class CustomerWriter : ICustomerRepository
    {
        private readonly string _connectionsString;
        private readonly Customer _customer;
        private readonly Service _service;
        private readonly Employee _employee;
        private readonly DateTime _appointmentDateTime;
        private readonly TimeSpan _executionTime;

        private Customer _existingCustomer = null;

        public bool IsRecordingSuccessful { get; private set; }

        public CustomerWriter(string connectionsString, Customer customer, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan executionTime)
        {
            _connectionsString = connectionsString;
            _customer = customer;
            _service = service;
            _employee = employee;
            _appointmentDateTime = appointmentDateTime;
            _executionTime = executionTime;
        }

        public async Task<bool> RecordCustomerAsync()
        {
            try
            {
                using (WritingDbContext context = new WritingDbContext(_connectionsString))
                {
                    GetExistingCustomerAsync(context);
                    CreateOrUpdateCustomerAsync(context);
                    EnsureMasterIsAvailableAsync(context);

                    TimeSpan _endTime = _appointmentDateTime.TimeOfDay + _executionTime;

                    CustomerRecords clientService = new CustomerRecords { CustomerId = _customer.Id, ServiceId = _service.Id, ServiceDateTime = _appointmentDateTime, ServiceEndTime = _endTime };
                    context.Client_Service.Add(clientService);

                    EmployeeRecords employeeService = new EmployeeRecords { EmployeeId = _employee.Id, ServiceId = _service.Id, ServiceDateTime = _appointmentDateTime, ServiceEndTime = _endTime };
                    context.Employee_Service.Add(employeeService);

                    await context.SaveChangesAsync();

                    IsRecordingSuccessful = true;
                    return true;
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                Console.WriteLine(ex.Message);
                IsRecordingSuccessful = false;
                return false;
            }
        }

        private void GetExistingCustomerAsync(WritingDbContext context)
        {
            _existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerFullName == _customer.CustomerFullName && c.CustomerBirthDate == _customer.CustomerBirthDate);
        }

        private void CreateOrUpdateCustomerAsync(WritingDbContext context)
        { 
            if (_existingCustomer == null)
            {
                _customer.CustomerIsNew = true;
                context.Customers.Add(_customer);
            }
            else
            {
                _existingCustomer.CustomerIsNew = false;
                _customer.Id = _existingCustomer.Id;
                context.Entry(_existingCustomer).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        private void EnsureMasterIsAvailableAsync(WritingDbContext context)
        {
            var employeeRecords = context.Employee_Service
                                   .Where(es => es.EmployeeId == _employee.Id && DbFunctions.TruncateTime(es.ServiceDateTime) == _appointmentDateTime.Date)
                                   .ToList();
            bool isMasterBusy = employeeRecords.Any(es => es.ServiceDateTime <= _appointmentDateTime && es.ServiceEndTime >= _appointmentDateTime.TimeOfDay);

            if (isMasterBusy)
            {
                IsRecordingSuccessful = false;
                throw new InvalidOperationException("Мастер не доступен в выбранное время.");
            }
            else
            {
                IsRecordingSuccessful = true;
            }
        }
    }
}
