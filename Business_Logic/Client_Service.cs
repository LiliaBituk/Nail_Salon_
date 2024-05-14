using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    public class Client_Service
    {
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int IdCustomer { get; set; }

        public virtual Service Service { get; set; }
        [ForeignKey("Service")]
        public int IdService { get; set; }

        public DateTime DateTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
