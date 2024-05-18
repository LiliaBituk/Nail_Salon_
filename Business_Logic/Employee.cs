//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Business_Logic
//{
//    public class Employee
//    {
//        [Key]
//        [Column("id")]
//        public int Id { get; set; }

//        [Column("FullName")]
//        public string EmployeeFullName { get; set; }

//        [Column("EmploymentContractNumber")]
//        public string EmploymentContractNumber { get; set; }

//        [Column("BirthDate")]
//        public DateTime EmployeeBirthDate { get; set; }

//        [Column("TypeService")]
//        public string EmployeeTypeService { get; set; }

//        [Column("PermanentEmployee")]
//        public bool PermanentEmployeeStatus { get; set; }

//        [Column("PhoneNumber")]
//        public decimal EmployeePhoneNumber { get; set; }

//        [NotMapped]
//        public int Score { get; set; }

//        [InverseProperty("Employee")]
//        public ICollection<EmployeeRecords> IEmployeeRecords { get; set; }
//    }
//}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("FullName")]
        public string EmployeeFullName { get; set; }

        [Column("PhoneNumber")]
        public decimal EmployeePhoneNumber { get; set; }

        [Column("TypeService")]
        public string EmployeeTypeService { get; set; }

        [Column("EmploymentContractNumber")]
        public string EmploymentContractNumber { get; set; }

        [Column("BirthDate")]
        public DateTime EmployeeBirthDate { get; set; }

        [Column("PermanentEmployee")]
        public bool PermanentEmployeeStatus { get; set; }

        [NotMapped]
        public int Score { get; set; }

        [InverseProperty("Employee")]
        public ICollection<EmployeeRecords> EmployeeRecords { get; set; }
    }
}
