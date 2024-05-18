//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Business_Logic
//{
//    [Table("Client_Service")]
//    public class CustomerRecords
//    {
//        [Key]
//        [Column("id")]
//        public int Id { get; set; }

//[ForeignKey("Customer")]
//        [Column("CustomerId")]
//       // public virtual Customer Customer { get; set; }

//        public int CustomerId { get; set; }

//        [ForeignKey("Service")]
//        [Column("ServiceId")]
//       // public virtual Service Service { get; set; }

//        public int ServiceId { get; set; }

//        [Column("DateTime")]
//        public DateTime ServiceDateTime { get; set; }

//        [Column("EndTime")]
//        public TimeSpan ServiceEndTime { get; set; }
//    }
//}

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
