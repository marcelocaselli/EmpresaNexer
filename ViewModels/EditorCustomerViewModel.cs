using System.ComponentModel.DataAnnotations;

namespace EmpresaNexer.ViewModels
{
    public class EditorCustomerViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "deve conter entre 3 e 60 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "o email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O endereço é obrigatório")]
        public string Address { get; set; }
    }
}