using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Application.Services;
using ZenLogAPI.Domain.Interfaces;
using ZenLogAPI.Infra.Data.AppData;
using ZenLogAPI.Infra.Data.HealthCheck;
using ZenLogAPI.Infra.Data.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(conf => {
                 conf.EnableAnnotations();
                 conf.ExampleFilters();
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);

                options.AssumeDefaultVersionWhenUnspecified = true;

                options.ReportApiVersions = true;

                options.ApiVersionReader = Asp.Versioning.ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"),
                    new QueryStringApiVersionReader("api-version")
                );
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        }
    }
}
