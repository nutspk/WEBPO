using Microsoft.Extensions.DependencyInjection;
using WEBPO.Domain.UnitOfWork;
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;
using WEBPO.Domain.Repositories;
using WEBPO.Core.Interfaces;
using WEBPO.Core.Services;
using Microsoft.AspNetCore.Http;

namespace WEBPO.Core.Persistances
{
    public static class Bootstrap
    {
        public static IServiceCollection AddBootstraper(this IServiceCollection services)
        {

            services.AddUnitOfWork<AppDbContext>();
            services.AddCustomRepository<MS_USER, UserRepository>();
            services.AddCustomRepository<MS_MNFUNC, MenuRepository>();
            services.AddCustomRepository<TR_PUB, PublicMessageRepository>();
            services.AddCustomRepository<MS_PIC, ContactPersonRepository>();

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPublicMessageService, PublicMessageService>();
            services.AddScoped<IContactPersonService, ContactPersonService>();

            services.AddTransient<IUserResolverService, UserResolverService>();

            return services;
        }
    }
}
