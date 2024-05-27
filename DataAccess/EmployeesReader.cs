using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EmployeesReader : IEmployeeRepository
    {
        private readonly ReadingDbContext _context;

        public EmployeesReader(DbContextOptions<ReadingDbContext> options)
        {
            _context = new ReadingDbContext(options);
        }

        public async Task<List<Employee>> GetAllEmployeesAndCountScore()
        {
            try
            {
                var employeesWithScore = await _context.Employees
                    .OrderByDescending(e => e.EmployeeRecords
                        .Where(es => es.ServiceDateTime >= DateTime.Now.AddMonths(-2))
                        .Select(es => new { es.ServiceId, es.ServiceDateTime, es.EmployeeId, es.ServiceEndTime })
                        .Count())
                    .ThenBy(e => e.EmployeeFullName)
                    .Select(e => new Employee
                    {
                        EmployeeFullName = e.EmployeeFullName,
                        EmployeeTypeService = e.EmployeeTypeService,
                        EmployeePhoneNumber = e.EmployeePhoneNumber,
                        Score = e.EmployeeRecords
                            .Where(es => es.ServiceDateTime >= DateTime.Now.AddMonths(-2))
                            .Select(es => new { es.ServiceId, es.ServiceDateTime, es.EmployeeId, es.ServiceEndTime })
                            .Count()
                    })
                    .ToListAsync();

                return employeesWithScore;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Employee>> GetEmployeesByServiceType(string serviceType)
        {
            try
            {
                return await _context.Employees
                    .Where(e => e.EmployeeTypeService == serviceType)
                    .Select(e => new Employee
                    {
                        Id = e.Id,
                        EmployeeFullName = e.EmployeeFullName,
                        EmployeeTypeService = e.EmployeeTypeService,
                        EmploymentContractNumber = e.EmploymentContractNumber,
                        EmployeeBirthDate = e.EmployeeBirthDate,
                        PermanentEmployeeStatus = e.PermanentEmployeeStatus,
                        EmployeePhoneNumber = e.EmployeePhoneNumber
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
