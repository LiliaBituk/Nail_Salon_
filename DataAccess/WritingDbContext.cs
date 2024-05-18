using Business_Logic;
using System.Data.Entity;

namespace DataAccess
{
    public class WritingDbContext : DbContext
    {
        public WritingDbContext(string connectionString) : base(connectionString) { }

        public DbSet<Service> Services { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }


        public DbSet<CustomerRecords> Client_Service { get; set; }

        public DbSet<EmployeeRecords> Employee_Service { get; set; }
    }
}

