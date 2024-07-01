using System.ComponentModel.DataAnnotations;

namespace EmpresaNexer.ViewModels
{
    public class EditorBillingLineViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Description { get; set; }
    }
}