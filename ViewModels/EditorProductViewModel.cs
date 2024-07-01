using System.ComponentModel.DataAnnotations;

namespace EmpresaNexer.ViewModels
{
    public class EditorProductViewModel
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        public string NameProduct { get; set; }
    }
}