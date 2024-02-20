using DefineFIT.Application.Services;
using DefineFIT.Application.Services.Interfaces;
using DefineFIT.Domain.Repositories;
using DefineFIT.Infra;
using DefineFIT.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DefineFIT.Api.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            //Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
