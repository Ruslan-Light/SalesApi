using Application.EntityDbContext;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiContext _context;

        public AuthService(IApiContext context)
        {
            _context = context;
        }

        public async Task<ClaimsIdentity> GetIdentityAsync(string userName, string password)
        {
            var user = await _context.Buyers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Name == userName)
                ?? throw new NotFoundException("Пользователь не найден в системе.");

            var passwordVerificationResult = new PasswordHasher<Buyer?>().VerifyHashedPassword(user, user.Password, password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new AccessException("Не верно введен пароль.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                ClaimsIdentity.DefaultNameClaimType);

            return claimsIdentity;
        }
    }
}
