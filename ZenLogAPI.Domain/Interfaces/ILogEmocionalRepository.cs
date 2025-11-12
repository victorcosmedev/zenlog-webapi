using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Domain.Interfaces
{
    public interface ILogEmocionalRepository
    {
        Task<LogEmocionalEntity?> AdicionarAsync(LogEmocionalEntity log);
        Task<LogEmocionalEntity?> EditarAsync(int id, LogEmocionalEntity log);
        Task<LogEmocionalEntity?> RemoverAsync(int id);
        Task<LogEmocionalEntity?> BuscarPorIdAsync(int id);
        Task<PageResultModel<IEnumerable<LogEmocionalEntity>?>> ListarPorColaboradorAsync(int colaboradorId, int pageNumber = 1, int pageSize = 10);
    }
}
