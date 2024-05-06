using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ScheduleReader
    {
        private readonly ScheduleReaderDbContext _context;

        public ScheduleReader(DbContextOptions<ScheduleReaderDbContext> options)
        {
            _context = new ScheduleReaderDbContext(options);
        }

        public List<Schedule> GetSchedule(DateTime selectedDate)
        {
            try
            {
                List<Schedule> items = new List<Schedule>();

                var query = (from cs in _context.Client_Service
                             join c in _context.Customers on cs.idCustomer equals c.id
                             join s in _context.Services on cs.idService equals s.id
                             join es in _context.Employee_Service on s.id equals es.idService
                             join e in _context.Employees on es.idEmployee equals e.id
                             where cs.dateTime.Date == selectedDate.Date
                             select new
                             {
                                 CustomerName = c.fullName,
                                 ServiceName = s.name,
                                 StartDateTime = cs.dateTime,
                                 EmployeeName = e.fullName,
                                 Price = s.price
                             }).Distinct();

                foreach (var result in query)
                {
                    Schedule item = new Schedule
                    {
                        customerName = result.CustomerName,
                        serviceName = result.ServiceName,
                        startDateTime = result.StartDateTime,
                        employeeName = result.EmployeeName,
                        price = result.Price
                    };
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Schedule>();
            }
        }
    }

    public class ScheduleReaderDbContext : DbContext
    {
        public DbSet<Client_Service> Client_Service { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee_Service> Employee_Service { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Schedule> Schedule { get; set; }

        public ScheduleReaderDbContext(DbContextOptions<ScheduleReaderDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasKey(s => s.id);
        }
    }
}