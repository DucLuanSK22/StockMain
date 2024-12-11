using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.BE.Core.DL;
using Stock.BE.Infrastructure.Repository;
using Stock.Infrastructure;
namespace Stock.BE.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuation)
    {
        services.AddScoped<IStockDL, StockDL>();
        string? connectionString = configuation.GetConnectionString("ESP");
        connectionString = connectionString ?? "";
        services.AddScoped<IUnitOfWork>((provider => new UnitOfWork(connectionString)));
    }
}
