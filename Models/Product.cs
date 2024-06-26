namespace EmpresaNexer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public BillingLine BillingLine { get; set; }
    }
}