using Business_Logic;

namespace DataAccess
{
    public interface ICustomerRepository
    {
        Task RecordCustomerAsync(Customer customer, Service service, Employee employee, DateTime appointmentDateTime, TimeSpan endTime);
        bool AddOrUpdateCustomer(Customer customer);
        bool IsEmployeeAvailable(int employeeId, DateTime appointmentDateTime);
    }
}
