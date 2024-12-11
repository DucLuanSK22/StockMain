using Quartz;

namespace Stock.BE.Jobs;

[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
        }
        catch (Exception)
        {
            throw;
        }
    }
}