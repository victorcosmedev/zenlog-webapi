using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Infra.Data.AppData;

namespace ZenLogAPI.Infra.Data.HealthCheck
{
    public class OracleHealthCheck : IHealthCheck
    {
        private readonly ApplicationContext _context;
        public OracleHealthCheck(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Empresas
                    .AsNoTracking()
                    .Take(1)
                    .AnyAsync(cancellationToken);

                return HealthCheckResult.Healthy("Banco de dados está online");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Banco de dados está offline", ex);
            }
        }
    }
}
