namespace EmpresaNexer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public IList<BillingLine> BillingLines { get; set; }
    }
}