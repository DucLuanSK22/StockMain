using Microsoft.AspNetCore.Mvc;
using Stock.BE.Core.DL;
using Stock.BE.Core.DTO;
using Stock.BE.Core.Enum;
using Stock.BE.Core.Model;
using Stock.BE.Infrastructure.Repository;

namespace Stock.BE.Host.Controllers;

[Route("api/stock")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly IStockDL _stockDL;
    private readonly IUserDL _userDL;
    public StocksController(IStockDL stockDL, IUserDL userDL)
    {
        _stockDL = stockDL;
        _userDL = userDL;
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
    [HttpGet("add_cash")]
    public async Task<IActionResult> AddCashByUser(Guid userId)
    {
        await _stockDL.AddCashByUser(userId);
        return Ok(1);
    }

    [HttpGet("get_popular_stock")]
    public async Task<IActionResult> GetPopularStockAsync()
    {
        var result = await _stockDL.GetPopularStockAsync();
        return Ok(result);
    }
    [HttpGet("get_asset_history_by_user")]
    public async Task<IActionResult> GetAssetHistoryByUserAsync(Guid userId, PeriodEnum periodEnum)
    {
        var result = await _stockDL.GetAssetHistoryByUserAsync(userId, periodEnum);
        return Ok(result);
    }


    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(UserDto userDto)
    {
        await _userDL.InsertUser(userDto);
        return Ok("Tạo tài khoản thành công");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(UserLoginParam userLoginParam)
    {
        var user = await _userDL.GetUserByEmailAsync(userLoginParam.email);

        if (user == null)
        {
            return NotFound("Email người dùng không tồn tại trên hệ thống!");
        }

        if (user.password != userLoginParam.password)
        {
            return BadRequest("Mật khẩu đăng nhập không chính xác. Vui lòng kiểm tra lại");
        }

        return Ok(user);
    }
}
