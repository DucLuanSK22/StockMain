using Microsoft.Extensions.Options;
using Quartz;

namespace Stock.BE.Jobs
{
    public class UpdateAssetBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(UpdateAssetBackgroundJob));
            options
                .AddJob<UpdateAssetBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInHours(24).RepeatForever()));
        }
    }
}