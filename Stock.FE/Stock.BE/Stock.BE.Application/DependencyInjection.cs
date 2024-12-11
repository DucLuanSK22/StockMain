using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.BE.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMapper, Mapper>();
        }
    }
}
