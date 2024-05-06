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

        public List<Employee> GetAllEmployeesAndCountScore()
        {
            try
            {
                return _context.Employees
                    .OrderByDescending(e => e.Employee_Service
                        .Where(es => es.dateTime >= DateTime.Now.AddMonths(-2))
                        .Select(es => new { es.idService, es.dateTime, es.idEmployee, es.endTime })
                        .Distinct()
                        .Count())
                    .ThenBy(e => e.fullName)
                    .Select(e => new Employee
                    {
                        fullName = e.fullName,
                        typeService = e.typeService,
                        phoneNumber = e.phoneNumber,
                        score = e.Employee_Service
                            .Where(es => es.dateTime >= DateTime.Now.AddMonths(-2))
                            .Select(es => new { es.idService, es.dateTime, es.idEmployee, es.endTime })
                            .Distinct()
                            .Count()
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Employee>();
            }
        }

        public List<Employee> GetEmployeesByServiceType(string serviceType)
        {
            try
            {
                return _context.Employees
                    .Where(e => e.typeService == serviceType)
                    .Select(e => new Employee
                    {
                        id = e.id,
                        fullName = e.fullName,
                        typeService = e.typeService,
                        employmentContractNumber = e.employmentContractNumber,
                        birthDate = e.birthDate,
                        permanentEmployee = e.permanentEmployee,
                        phoneNumber = e.phoneNumber
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Employee>();
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
