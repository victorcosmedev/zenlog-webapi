using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Models.OperationResult;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Application.Interfaces
{
    public interface IColaboradorApplicationService
    {
        Task<OperationResult<ColaboradorDto?>> AdicionarAsync(ColaboradorDto colaboradorDto);
        Task<OperationResult<ColaboradorDto?>> EditarAsync(int id, ColaboradorDto colaboradorDto);
        Task<OperationResult<ColaboradorDto?>> RemoverAsync(int id);
        Task<OperationResult<ColaboradorDto?>> BuscarPorIdAsync(int id);
        Task<OperationResult<ColaboradorDto?>> BuscarPorEmailAsync(string email);
        Task<OperationResult<ColaboradorDto?>> BuscarPorCpfAsync(string cpf);
        Task<OperationResult<ColaboradorDto?>> BuscarPorMatriculaAsync(string numeroMatricula);
        Task<OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>> ListarAsync(int pageNumber = 1, int pageSize = 10);
        Task<OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>> ListarPorEmpresaAsync(int empresaId, int pageNumber = 1, int pageSize = 10);
    }
}
