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

        public async Task<List<Schedule>> GetSchedule(DateTime selectedDate)
        {
            try
            {
                List<Schedule> items = new List<Schedule>();

                var query = from cs in _context.Client_Service
                            join c in _context.Customers on cs.IdCustomer equals c.Id
                            join s in _context.Services on cs.IdService equals s.Id
                            join es in _context.Employee_Service on s.Id equals es.IdService
                            join e in _context.Employees on es.IdEmployee equals e.Id
                            where cs.DateTime.Date == selectedDate.Date 
                            select new Schedule
                            {
                                CustomerFullName = c.FullName,
                                ServiceName = s.Name,
                                StartDateTime = cs.DateTime,
                                EmployeeName = e.FullName,
                                Price = s.Price
                            };

                var schedule = await query.Distinct().OrderBy(cs => cs.StartDateTime).ToListAsync();

                return schedule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
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
                .HasKey(s => s.Id);
        }
    }
}