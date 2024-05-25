using Business_Logic;

namespace DataAccess
{
    public interface IScheduleRepository
    {
        Task<List<Schedule>> GetSchedule(DateTime selectedDate);
    }
}
