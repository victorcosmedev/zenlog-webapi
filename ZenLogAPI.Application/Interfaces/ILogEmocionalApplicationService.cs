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
    public interface ILogEmocionalApplicationService
    {
        Task<OperationResult<LogEmocionalDto?>> AdicionarAsync(LogEmocionalDto logDto);
        Task<OperationResult<LogEmocionalDto?>> EditarAsync(int id, LogEmocionalDto logDto);
        Task<OperationResult<LogEmocionalDto?>> RemoverAsync(int id);
        Task<OperationResult<LogEmocionalDto?>> BuscarPorIdAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<LogEmocionalDto>>>> ListarPorColaboradorAsync(int colaboradorId, int pageNumber = 1, int pageSize = 10);
        Task<OperationResult<IEnumerable<LogEmocionalDto>?>> ListarTodosAsync();
    }
}
