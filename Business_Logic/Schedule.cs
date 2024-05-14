using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class Schedule 
    {
        public int Id { get; set; }

        public string CustomerFullName { get; set; }

        public string ServiceName { get; set; }

        public DateTime StartDateTime { get; set; }

        public string EmployeeName { get; set; }

        public decimal Price { get; set; }

    }
}
