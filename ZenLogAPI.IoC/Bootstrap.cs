using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Application.Services;
using ZenLogAPI.Domain.Interfaces;
using ZenLogAPI.Infra.Data.AppData;
using ZenLogAPI.Infra.Data.HealthCheck;
using ZenLogAPI.Infra.Data.Repositories;

namespace ZenLogAPI.IoC
{
    public class Bootstrap
    {
        public static void AddIoC(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=rm558856;Password=fiap25;";
            services.AddDbContext<ApplicationContext>(opt => {
                opt.UseOracle(connectionString);
            });

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
                .AddCheck<OracleHealthCheck>("oracle_query", tags: new[] { "ready" });

            services.AddTransient<IEmpresaApplicationService, EmpresaApplicationService>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();

            services.AddTransient<ILogEmocionalApplicationService, LogEmocionalApplicationService>();
            services.AddTransient<ILogEmocionalRepository, LogEmocionalRepository>();

            services.AddTransient<IColaboradorApplicationService, ColaboradorApplicationService>();
            services.AddTransient<IColaboradorRepository, ColaboradorRepository>();
        }
    }
}
