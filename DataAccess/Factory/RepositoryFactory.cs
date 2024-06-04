using Business_Logic;

namespace DataAccess
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string _connectionString;

        public RepositoryFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IServiceRepository CreateServiceRepository()
        {
            return new ServiceReader(_connectionString);
        }

        public IEmployeeRepository CreateEmployeeRepository()
        {
            return new EmployeesReader(_connectionString);
        }

        public ICustomerRepository CreateCustomerRepository(Customer customer, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan executionTime)
        {
            return new CustomerWriter(_connectionString);
        }

        public IScheduleRepository CreateScheduleRepository()
        {
            return new ScheduleReader(_connectionString);
        }
    }

}
