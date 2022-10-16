using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        public Task<ClaimsIdentity> GetIdentityAsync(string userName, string password);
    }
}
