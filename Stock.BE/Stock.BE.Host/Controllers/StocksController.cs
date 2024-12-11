using Microsoft.AspNetCore.Mvc;
using Stock.BE.Core.DL;
using Stock.BE.Core.Enum;

namespace Stock.BE.Host.Controllers;

[Route("api/stock")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStockDL _stockDL;

    public StocksController(IStockDL stockDL)
    {
        _stockDL = stockDL;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        var result = await _stockDL.GetAllAsync();
        return Ok(result);
    }
    [HttpGet("get_stock_by_period")]
    public async Task<IActionResult> GetStockByPeriodAsync(Guid stockId, PeriodEnum periodEnum)
    {
        var result = await _stockDL.GetStockByPeriodAsync(stockId, periodEnum);
        return Ok(result);
    }

}
