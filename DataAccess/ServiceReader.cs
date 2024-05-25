using Business_Logic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ServiceReader : IServiceRepository
    {
        private readonly ReadingDbContext _context;

        public ServiceReader(DbContextOptions<ReadingDbContext> options)
        {
            _context = new ReadingDbContext(options);
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
}
