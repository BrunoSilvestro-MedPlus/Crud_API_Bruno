using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Crud_API_Bruno.Application.ViewModels;
using Crud_API_Bruno.Domain.Users;

namespace Crud_API_Bruno.Application.Services
{
    public class JwtIdentityAuthService : IAuthService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public JwtIdentityAuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResult> Authenticate(SignInAction source)
        {
            var result = await _signInManager.PasswordSignInAsync(source.Email, source.Password, false, true);

            if (!result.Succeeded)
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    ReasonOfFail = "Usuário ou senha inválidos"
                };
            }

            return await GenerateToken(source.Email);
        }

        public async Task<AuthenticationResult> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));
            identityClaims.AddClaim(new Claim("Crud_UserId", user.Id.ToString()));
            identityClaims.AddClaim(new Claim("Crud_Role", Convert.ToInt32(user.Role).ToString()));
            identityClaims.AddClaim(new Claim("Crud_Name", user.Name));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("EncryptKey"));
            var createdAt = DateTime.Now;
            var valid = createdAt.AddHours(6);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = valid,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return new AuthenticationResult()
            {
                    Success = true,
                    AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                    CreatedAt = createdAt,
                    ExpiresAt = valid,

            };
            
        }

        public async Task<AuthenticationResult> Register(RegisterAction source)
        {
            var user = new User()
            {
                Email = source.Email,
                UserName = source.Email,
                Name = source.Name,
                Role = source.Role
            };

            var createResult = await _userManager.CreateAsync(user, source.Password);

            if (createResult.Succeeded)
            {
                return await GenerateToken(user.Email);
            }

            return new AuthenticationResult()
            {
                Success = false,
                ReasonOfFail = createResult.Errors.ElementAtOrDefault(0).Description
            };
        }
    }
}