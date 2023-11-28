using System;
using Contact.Application.Interfaces;
using Contact.Infrastructure.Data;
using Contact.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.API.UnitTests.Helper
{
    public static class ServiceHelper
    {
        private static IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IUserContactService, UserContactService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJWTService, JWTService>();


            var myconfiguration = new Dictionary<string, string>();

            services.AddDbContext<ApplicationDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();


            services.AddSingleton<IConfiguration>(configuration);

            return services.BuildServiceProvider();
        }


        public static T GetRequiredService<T>()
        {
            var provider = Provider();

            return provider.GetRequiredService<T>();
        }
    }
}

