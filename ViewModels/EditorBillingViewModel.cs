using System.ComponentModel.DataAnnotations;

namespace EmpresaNexer.ViewModels
{
    public class EditorBillingViewModel
    {
        [Required(ErrorMessage = "A data de vencimento é obrigatório")]
        public DateTime DataVencimento { get; set; }
    }
}