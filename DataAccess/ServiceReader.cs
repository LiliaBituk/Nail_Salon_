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

        public async Task<List<Service>> GetAllServices()
        {
            try
            {
                return await _context.Services
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

    public class ServiceReaderDbContext : DbContext
    {
        public DbSet<Service> Services { get; set; }

        public ServiceReaderDbContext(DbContextOptions<ServiceReaderDbContext> options) : base(options) { }
    }
}
