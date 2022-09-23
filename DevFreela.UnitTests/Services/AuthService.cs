using DevFreela.Core.Enums;
using DevFreela.Core.Services;

namespace DevFreela.UnitTests.Services
{
    public class AuthService : IAuthService
    {
        public string ComputeSha256Hash(string password)
        {
            return password;
        }

        public string GenerateJwtToken(string email, UserRoleEnum role)
        {
            return "token_de_teste";
        }
    }
}
