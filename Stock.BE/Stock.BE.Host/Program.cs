
using Microsoft.Extensions.DependencyInjection;
using Stock.BE.Host.Middleware;
using Stock.BE.Infrastructure;
using Stock.BE.Jobs;
namespace Stock.BE.Host;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
        {
            build.WithOrigins("http://localhost:5173")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
        }));
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddJobs();
        builder.Services.AddInfrastructure(builder.Configuration);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("MyCors");
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        app.UseMiddleware<ExceptionMiddleware>();

        app.Run();
    }
}