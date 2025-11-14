using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Infra.Data.AppData
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            string apiProjectBasePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../ZenLogAPI/"));


            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectBasePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Oracle");

            optionsBuilder.UseOracle(connectionString);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
