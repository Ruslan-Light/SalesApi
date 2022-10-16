using Application.EntityDbContext;
using Application.UseCases.Login;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Signin
{
    public class RegisterUserQuery : Profile, IRequest<AuthVm>
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public RegisterUserQuery()
        {
            CreateMap<RegisterUserQuery, Buyer>();
        }

        private class Handler : IRequestHandler<RegisterUserQuery, AuthVm>
        {
            private readonly IApiContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<RegisterUserQuery> _logger;
            private readonly IMediator _mediator;

            public Handler(IApiContext context, 
                IMapper mapper, 
                ILogger<RegisterUserQuery> logger, 
                IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<AuthVm> Handle(RegisterUserQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _mapper.Map<Buyer>(request)
                        ?? throw new Exception("Во время регистрации произошла ошибка.");

                    var userEmailExist = await _context.Buyers.AnyAsync(u => u.Name.Equals(user.Name));
                    if (userEmailExist)
                    {
                        throw new ValidationException("Пользователь с таким Email уже существует.");
                    }

                    var hashedPassword = new PasswordHasher<Buyer?>().HashPassword(user, request.Password);
                    user.Password = hashedPassword;

                    _context.Buyers.Add(user);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"Добавлен новый пользователь {user.Name}");

                    var auth = new AuthenticationQuery
                    {
                        Name = request.Name,
                        Password = request.Password
                    };

                    return await _mediator.Send(auth);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
