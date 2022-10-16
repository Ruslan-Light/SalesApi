using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Options
{
    public class AuthOptions
    {
        // издатель токена
        public string ISSUER { get; init; }
        // потребитель токена
        public string AUDIENCE { get; init; }
        // ключ для шифрации
        public string KEY { get; init; }
        // время жизни токена
        public int LIFETIME { get; init; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
