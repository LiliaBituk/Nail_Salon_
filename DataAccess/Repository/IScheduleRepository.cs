using Business_Logic;

namespace DataAccess
{
    public interface IScheduleRepository
    {
        Task<List<VisitLogs>> GetSchedule(DateTime selectedDate);
        Task<bool> DeleteAppointment(int appointmentId);
    }
}
