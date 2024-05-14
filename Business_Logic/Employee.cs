
namespace Business_Logic
{
    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string EmploymentContractNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string TypeService { get; set; }

        public bool PermanentEmployee { get; set; }

        public decimal PhoneNumber { get; set; }

        public int Score { get; set; }

        public ICollection<Employee_Service> Employee_Service { get; set; }
    }
}
