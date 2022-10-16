using Application.EntityDbContext;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Services
{
    public class UserManagingService : IUserManagingService
    {
        private readonly IApiContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMemoryCache _cache;

        public UserManagingService(IApiContext context,
            IHttpContextAccessor accessor,
            IMemoryCache cache)
        {
            _context = context;
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _cache = cache;
        }

        public ClaimsPrincipal ClaimsUser => _accessor.HttpContext.User
            ?? throw new ArgumentNullException(nameof(ClaimsPrincipal));

        public Buyer CurrentUser
        {
            get
            {
                Buyer user = null;

                if (ClaimsUser.Identity.IsAuthenticated && !_cache.TryGetValue(ClaimsUser.Identity.Name, out user))
                {
                    user = _context.Buyers
                        .Include(b => b.Sales)
                        ?.FirstOrDefault(u => u.Name.Equals(ClaimsUser.Identity.Name));

                    _cache.Set(ClaimsUser.Identity.Name, user,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
                }

                return user;
            }
        }
    }
}
