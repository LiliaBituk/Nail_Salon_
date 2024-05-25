using Business_Logic;

namespace DataAccess
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAndCountScore();
        Task<List<Employee>> GetEmployeesByServiceType(string serviceType);
    }
}
