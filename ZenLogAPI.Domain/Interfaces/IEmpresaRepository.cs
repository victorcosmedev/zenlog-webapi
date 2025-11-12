using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<EmpresaEntity?> AdicionarAsync(EmpresaEntity empresa);
        Task<EmpresaEntity?> EditarAsync(int id, EmpresaEntity empresa);
        Task<EmpresaEntity?> RemoverAsync(int id);
        Task<EmpresaEntity?> BuscarPorIdAsync(int id);
        Task<PageResultModel<IEnumerable<EmpresaEntity>>> ListarAsync(int pageNumber = 1, int pageSize = 10);
    }
}
