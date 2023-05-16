namespace Customer.Api.Models
{
    public class Customers
    {
        public Guid ID { get; set; }
        public string CustomerName { get; set; }
        public string Geder { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}
