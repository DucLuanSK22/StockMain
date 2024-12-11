using Quartz;
using Stock.BE.Core.DL;
namespace Stock.BE.Jobs;

[DisallowConcurrentExecution]
public class UpdateAssetBackgroundJob : IJob
{
    private readonly IStockDL _stockDL;
    public UpdateAssetBackgroundJob(IStockDL stockDL)
    {
        _stockDL = stockDL;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        await _stockDL.UpdateAssetUserAsync();
    }
}