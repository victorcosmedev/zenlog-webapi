using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Application.DTOs.Empresa;
using ZenLogAPI.Domain.Models.OperationResult;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Application.Interfaces
{
    public interface IEmpresaApplicationService
    {
        Task<OperationResult<EmpresaDto?>> AdicionarAsync(EmpresaDto empresaDto);
        Task<OperationResult<EmpresaDto?>> EditarAsync(int id, EmpresaDto empresaDto);
        Task<OperationResult<EmpresaDto?>> RemoverAsync(int id);
        Task<OperationResult<EmpresaDto?>> BuscarPorIdAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<EmpresaDto>>>> ListarAsync(int pageNumber = 1, int pageSize = 10);
    }
}
