using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserManagingService
    {
        public Buyer CurrentUser { get; }
    }
}
