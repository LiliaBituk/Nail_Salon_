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

        [Column("EmployeeId")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

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
