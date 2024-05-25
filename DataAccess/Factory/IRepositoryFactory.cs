using Business_Logic;

namespace DataAccess
{
    public interface IRepositoryFactory
    {
        IServiceRepository CreateServiceRepository();
        IEmployeeRepository CreateEmployeeRepository();
        ICustomerRepository CreateCustomerRepository(Customer customer, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan executionTime);
        IScheduleRepository CreateScheduleRepository();
    }
}
