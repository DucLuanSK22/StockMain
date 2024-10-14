using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Stock.BE.Jobs
{
    public static class DependencyInjection
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<LoggingBackgroundJobSetup>();
        }
    }
}
