namespace Business_Logic
{
    public class Schedule 
    {
        public Customer Customer { get; set; }

        public Service Service { get; set; }

        public Employee Employee { get; set; }

        public DateTime StartDateTime { get; set; }

        public decimal Price { get; set; }
    }
}
