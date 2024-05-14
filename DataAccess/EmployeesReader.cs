using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EmployeesReader
    {
        private readonly EmployeeReaderDbContext _context;

        public EmployeesReader(DbContextOptions<EmployeeReaderDbContext> options)
        {
            _context = new EmployeeReaderDbContext(options);
        }

        public async Task<List<Employee>> GetAllEmployeesAndCountScore()
        {
            try
            {
                var employeesWithScore = await _context.Employees
                    .OrderByDescending(e => e.Employee_Service
                        .Where(es => es.DateTime >= DateTime.Now.AddMonths(-2))
                        .Select(es => new { es.IdService, es.DateTime, es.IdEmployee, es.EndTime })
                        .Count())
                    .ThenBy(e => e.FullName)
                    .Select(e => new Employee
                    {
                        FullName = e.FullName,
                        TypeService = e.TypeService,
                        PhoneNumber = e.PhoneNumber,
                        Score = e.Employee_Service
                            .Where(es => es.DateTime >= DateTime.Now.AddMonths(-2))
                            .Select(es => new { es.IdService, es.DateTime, es.IdEmployee, es.EndTime })
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
                    .Where(e => e.TypeService == serviceType)
                    .Select(e => new Employee
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        TypeService = e.TypeService,
                        EmploymentContractNumber = e.EmploymentContractNumber,
                        BirthDate = e.BirthDate,
                        PermanentEmployee = e.PermanentEmployee,
                        PhoneNumber = e.PhoneNumber
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

    public class EmployeeReaderDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employee_Service> Employee_Service { get; set; }

        public EmployeeReaderDbContext(DbContextOptions<EmployeeReaderDbContext> options) : base(options) { }
    }
}
