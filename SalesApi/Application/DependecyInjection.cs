using Application.Behaviors;
using Application.Interfaces;
using Application.Options;
using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependecyInjection
    {
        public static void AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
        {
            // libs
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserManagingService, UserManagingService>();
            services.AddTransient<ISalesService, SalesService>();

            // behaviors
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // options
            services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddMemoryCache();
        }
    }
}
