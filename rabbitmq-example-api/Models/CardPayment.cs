namespace rabbitmq_example_api.Models
{
    public class CardPayment
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
    }
}