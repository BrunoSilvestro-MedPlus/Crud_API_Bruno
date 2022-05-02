using System.ComponentModel.DataAnnotations;
using Crud_API_Bruno.Domain.Users;

namespace Crud_API_Bruno.Application.ViewModels
{
    public class RegisterAction
    {
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        [Required(ErrorMessage = "Email não informado")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nome não informado")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Senha não informada")]
        public string Password { get; set; }
        [EnumDataType(typeof(Role), ErrorMessage = "Papel inválido")]
        public Role Role { get; set; }
    }
}