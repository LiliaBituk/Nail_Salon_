
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    public class Employee_Service
    {
        public int Id { get; set; }

        public virtual Employee Employee { get; set; }
        [ForeignKey("Employee")]
        public int IdEmployee { get; set; }

        public virtual Service Service { get; set; }
        [ForeignKey("Service")]
        public int IdService { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
