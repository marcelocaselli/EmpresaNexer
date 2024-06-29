namespace EmpresaNexer.Models
{
    public class BillingLine
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Billing Billing { get; set; }
        public Product Product { get; set; }
    }
}