using Business_Logic;

namespace DataAccess
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllServices();
    }
}
