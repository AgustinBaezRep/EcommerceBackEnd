using Microsoft.Extensions.DependencyInjection;
using Service.IServices;
using Service.Services;

namespace Api
{
    public class CompositeRoot
    {
        public static void DependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<IClientService, ClientService>();
        }
    }
}
