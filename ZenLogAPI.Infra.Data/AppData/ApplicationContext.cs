using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Infra.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ColaboradorEntity> Colaboradores { get; set; }
        public DbSet<EmpresaEntity> Empresas { get; set; }
        public DbSet<LogEmocionalEntity> LogsEmocionais { get; set; }

    }
}
