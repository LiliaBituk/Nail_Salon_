using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    [Table("Client_Service")]
    public class CustomerRecords
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [Column("CustomerId")]
        public int CustomerId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        [Column("ServiceId")]
        public int ServiceId { get; set; }

        [Column("dateTime")]
        public DateTime ServiceDateTime { get; set; }

        [Column("endTime")]
        public TimeSpan ServiceEndTime { get; set; }
    }
}
