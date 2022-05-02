using System.Threading.Tasks;
using Crud_API_Bruno.Application.ViewModels;

namespace Crud_API_Bruno.Domain.Users
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Authenticate(SignInAction source);
        Task<AuthenticationResult> Register(RegisterAction source);
        Task<AuthenticationResult> GenerateToken(string email);
    }
}