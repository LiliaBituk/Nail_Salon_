using Business_Logic;
using System.Data.Entity;
using System.Data.Entity.Core;

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
                using (WritingDbContext context = new WritingDbContext(_connectionsString))
                {
                    CustomerRecords clientAndService = new CustomerRecords { CustomerId = customer.Id, ServiceId = service.Id, ServiceDateTime = appointmentDateTime, ServiceEndTime = endTime };
                    context.Client_Service.Add(clientAndService);

                    EmployeeRecords employeeAndService = new EmployeeRecords { EmployeeId = employee.Id, ServiceId = service.Id, ServiceDateTime = appointmentDateTime, ServiceEndTime = endTime };
                    context.Employee_Service.Add(employeeAndService);

                    context.Client_Service.Add(clientAndService);
                    context.Employee_Service.Add(employeeAndService);

                    await context.SaveChangesAsync();
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool AddOrUpdateCustomer(Customer customer)
        {
            try
            {
                using (WritingDbContext context = new WritingDbContext(_connectionsString))
                {
                    var existingCustomer = context.Customers
                        .FirstOrDefault(c => c.CustomerFullName == customer.CustomerFullName && c.CustomerBirthDate == customer.CustomerBirthDate);

                    if (existingCustomer == null)
                    {
                        customer.CustomerIsNew = true;
                        context.Customers.Add(customer);
                    }
                    else
                    {
                        existingCustomer.CustomerIsNew = false;
                        customer.Id = existingCustomer.Id;
                        context.Entry(existingCustomer).State = EntityState.Modified;
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool IsEmployeeAvailable(int employeeId, DateTime appointmentDateTime)
        {
            using (WritingDbContext context = new WritingDbContext(_connectionsString))
            {
                var employeeRecords = context.Employee_Service
                                   .Where(es => es.EmployeeId == employeeId && DbFunctions.TruncateTime(es.ServiceDateTime) == appointmentDateTime.Date)
                                   .ToList();
               
                bool isEmployeeAvailable = !employeeRecords.Any(es => es.ServiceDateTime <= appointmentDateTime && es.ServiceEndTime >= appointmentDateTime.TimeOfDay);
                return isEmployeeAvailable;
            }
        }
    }
}
