namespace EmpresaNexer.Models
{
    public class Billing
    {
        public int Id { get; set; }
        public DateTime DataVencimento { get; set; }
        public Customer Customer { get; set; }
        public IList<BillingLine> BillingLines { get; set; }

    }
}