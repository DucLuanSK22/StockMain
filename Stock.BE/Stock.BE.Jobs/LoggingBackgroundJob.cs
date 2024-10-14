using Quartz;
using Stock.BE.Core.DL;
using Stock.BE.Core.Entity;
using System.Text.Json;

namespace Stock.BE.Jobs;

[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    private readonly IStockDL _stockDL;
    public LoggingBackgroundJob(IStockDL stockDL)
    {
        _stockDL = stockDL;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        await _stockDL.UpdateStockPriceChange();
    }
}