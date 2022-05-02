using System.ComponentModel.DataAnnotations;

namespace Crud_API_Bruno.Application.ViewModels
{
    public class SignInAction
    {
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        [Required(ErrorMessage = "Email não informado")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha não informada")]
        public string Password { get; set; }
    }
}