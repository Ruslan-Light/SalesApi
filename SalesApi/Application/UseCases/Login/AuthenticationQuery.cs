using Application.Interfaces;
using Application.Options;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Login
{
    public class AuthenticationQuery : IRequest<AuthVm>
    {
        public string Name { get; set; }
        public string Password { get; set; }

        private class Handler : IRequestHandler<AuthenticationQuery, AuthVm>
        {
            private readonly IAuthService _authService;
            private readonly ILogger<AuthenticationQuery> _logger;
            private readonly AuthOptions _authOptions;
            private readonly IUserManagingService _userManagingService;

            public Handler(
                IAuthService authService,
                ILogger<AuthenticationQuery> logger,
                IOptions<AuthOptions> authOptions,
                IUserManagingService userManagingService)
            {
                _authService = authService;
                _logger = logger;
                _authOptions = authOptions.Value;
                _userManagingService = userManagingService;
            }

            public async Task<AuthVm> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
            {
                var identity = await _authService.GetIdentityAsync(request.Name, request.Password);

                var now = DateTime.UtcNow;

                //создаем JWT-токен
                var jwt = new JwtSecurityToken(
                    issuer: _authOptions.ISSUER,
                    audience: _authOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(_authOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(_authOptions.SymmetricSecurityKey,
                        SecurityAlgorithms.HmacSha256));
                var encodingJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var auth = new AuthVm
                {
                    access_token = encodingJwt,
                    name = identity.Name,
                };

                _logger.LogInformation($"Пользователь {identity.Name} вошел в систему.");

                return auth;
            }
        }
    }
}
