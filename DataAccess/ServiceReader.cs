using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ServiceReader
    {
        private readonly ServiceReaderDbContext _context;

        public ServiceReader(DbContextOptions<ServiceReaderDbContext> options)
        {
            _context = new ServiceReaderDbContext(options);
        }

        public List<Service> GetAllServices()
        {
            try
            {
                return _context.Services
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Service>();
            }
        }
    }

    public class ServiceReaderDbContext : DbContext
    {
        public DbSet<Service> Services { get; set; }

        public ServiceReaderDbContext(DbContextOptions<ServiceReaderDbContext> options) : base(options) { }
    }
}
