using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    [Table("Employee_Service")]
    public class EmployeeRecords
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        //[ForeignKey("Employee")]
        //[Column("EmployeeId")]
        //public virtual Employee Employee { get; set; }
        //public int EmployeeId { get; set; }
        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }


        //[ForeignKey("Service")]
        //[Column("ServiceId")]
        //public virtual Service Service { get; set; }
        //public int ServiceId { get; set; }
        [Column("ServiceId")]
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }


        [Column("DateTime")]
        public DateTime ServiceDateTime { get; set; }

        [Column("EndTime")]
        public TimeSpan ServiceEndTime { get; set; }
    }

}

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Business_Logic
//{
//    public class EmployeeRecords
//    {
//        [Key]
//        public int Id { get; set; }

//        public int EmployeeId { get; set; }
//        public int ServiceId { get; set; }
//        public DateTime ServiceDateTime { get; set; }
//        public TimeSpan ServiceEndTime { get; set; }

//        [ForeignKey("EmployeeId")]
//        public Employee Employee { get; set; }

//        [ForeignKey("ServiceId")]
//        public Service Service { get; set; }
//    }
//}
