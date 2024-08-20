using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using MercadoPago.Config;
using Nop.Plugin.Payments.MercadoPago.Services;

namespace Nop.Plugin.Payments.MercadoPago.Infrastructure
{
    public class NopStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<OnboardingHttpClient>().WithProxy();
            services.AddSingleton<MercadoPagoService>(sp =>
            {
                MercadoPagoConfig.AccessToken = "YOUR_ACCESS_TOKEN";
                return new MercadoPagoService();
            });
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 3000;
    }
}
