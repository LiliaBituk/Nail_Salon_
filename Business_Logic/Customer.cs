﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("FullName")]
        public string CustomerFullName { get; set; }

        [Column("BirthDate")]
        public DateTime CustomerBirthDate { get; set; }

        [Column("PhoneNumber")]
        public decimal CustomerPhoneNumber { get; set; }

        [Column("IsNew")]
        public bool CustomerIsNew { get; set; }

        [InverseProperty("Customer")]
        public ICollection<CustomerRecords> CustomerRecords { get; set; }

        public bool IsRecordingSuccessful(bool isCustomerCreated, bool isEmployeeAvailable)
        {
            if (!isCustomerCreated)
            {
                throw new InvalidOperationException("Ошибка в записи клиента");
            }

            if (!isEmployeeAvailable)
            {
                throw new InvalidOperationException("Мастер занят в выбранное время");
            }

            return true;
        }
    }
}
