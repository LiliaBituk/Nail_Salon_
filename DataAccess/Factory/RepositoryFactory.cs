using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DbContextOptions<ReadingDbContext> _readingOptions;
        private readonly string _connectionString;

        public RepositoryFactory(DbContextOptions<ReadingDbContext> readingOptions, string connectionString)
        {
            _readingOptions = readingOptions;
            _connectionString = connectionString;
        }

        public IServiceRepository CreateServiceRepository()
        {
            return new ServiceReader(_readingOptions);
        }

        public IEmployeeRepository CreateEmployeeRepository()
        {
            return new EmployeesReader(_readingOptions);
        }

        public ICustomerRepository CreateCustomerRepository(Customer customer, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan executionTime)
        {
            return new CustomerWriter(_connectionString); //, customer, service, employee, appointmentDateTime, executionTime);
        }

        public IScheduleRepository CreateScheduleRepository()
        {
            return new ScheduleReader(_connectionString);
        }
    }

}
