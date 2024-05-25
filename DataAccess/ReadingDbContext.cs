using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ReadingDbContext : DbContext
    {
        public ReadingDbContext(DbContextOptions<ReadingDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<CustomerRecords> Client_Service { get; set; }

        public DbSet<EmployeeRecords> Employee_Service { get; set; }
    }
}
