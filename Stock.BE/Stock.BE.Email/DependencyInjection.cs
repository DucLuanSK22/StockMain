using Microsoft.Extensions.DependencyInjection;
namespace Stock.BE.Email
{
    public static class DependencyInjection
    {
        public static void AddEmail(this IServiceCollection services)
        {
            services.AddScoped<IEmail, EmailService>();
        }
    }
}
