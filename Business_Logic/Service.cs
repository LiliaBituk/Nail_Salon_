using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    public class Service
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("Name")]
        public string ServiceName { get; set; }

        [Column("Type")]
        public string ServiceType { get; set; }

        [Column("Price")]
        public decimal ServicePrice { get; set; }

        [Column("ExecutionTime")]
        public TimeSpan ServiceExecutionTime { get; set; }

        public ICollection<CustomerRecords> CustomerRecords { get; set; }

        public decimal GetDiscountedPrice(bool isNewCustomer)
        {
            decimal discountedPrice = ServicePrice;

            if (isNewCustomer)
            {
                discountedPrice *= 0.7m;
            }

            return discountedPrice;
        }
    }
}

