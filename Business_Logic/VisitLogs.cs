
namespace Business_Logic
{
    public class VisitLogs
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        //public Service Service { get; set; }

        public int ServiceId { get; set; }

        public int EmployeeId { get; set; }

       // public virtual Employee Employee { get; set; }

        public DateTime ServiceDateTime { get; set; }

        public TimeSpan ServiceEndTime { get; set; }
    }
}
