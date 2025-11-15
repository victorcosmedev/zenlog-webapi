using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Application.Mappers;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Interfaces;
using ZenLogAPI.Domain.Models.OperationResult;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Application.Services
{
    public class EmpresaApplicationService : IEmpresaApplicationService
    {
        private readonly IEmpresaRepository _repository;
        public EmpresaApplicationService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<EmpresaDto?>> AdicionarAsync(EmpresaDto empresaDto)
        {
            try
            {
                var entity = empresaDto.ToEntity();

                var empresa = await _repository.AdicionarAsync(entity);
                if(empresa is null)
                {
                    return OperationResult<EmpresaDto?>.Failure("Dados inválidos.", (int)HttpStatusCode.BadRequest);
                }
                var dto = empresa.ToDto();

                return OperationResult<EmpresaDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmpresaDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EmpresaDto?>> BuscarPorIdAsync(int id)
        {
            try
            {
                var entity = await _repository.BuscarPorIdAsync(id);
                if (entity is null)
                    return OperationResult<EmpresaDto?>.Failure("Empresa não encontrada.", (int)HttpStatusCode.NotFound);

                var dto = entity.ToDto();
                return OperationResult<EmpresaDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmpresaDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EmpresaDto?>> EditarAsync(int id, EmpresaDto empresaDto)
        {
            try
            {
                EmpresaEntity? dto = empresaDto.ToEntity();

                var success = await _repository.EditarAsync(id, dto);
                if (success is null)
                    return OperationResult<EmpresaDto?>.Failure("Empresa não encontrada.", (int)HttpStatusCode.NotFound);

                var resultDto = success.ToDto();
                return OperationResult<EmpresaDto?>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return OperationResult<EmpresaDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<EmpresaDto>>>> ListarAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pageResult = await _repository.ListarAsync(pageNumber, pageSize);

                if(pageResult.Items is null)
                {
                    return OperationResult<PageResultModel<IEnumerable<EmpresaDto>>>.Failure("Nenhuma empresa encontrada.", (int)HttpStatusCode.NotFound);
                }

                var dtos = pageResult.Items.Select(x => x.ToDto());
                var pageResultDto = new PageResultModel<IEnumerable<EmpresaDto>>
                {
                    Items = dtos,
                    TotalItens = pageResult.TotalItens,
                    NumeroPagina = pageResult.NumeroPagina,
                    TamanhoPagina = pageResult.TamanhoPagina
                };

                return OperationResult<PageResultModel<IEnumerable<EmpresaDto>>>.Success(pageResultDto);
            }
            catch (Exception ex)
            {
                return OperationResult<PageResultModel<IEnumerable<EmpresaDto>>>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<EmpresaDto?>> RemoverAsync(int id)
        {
            try
            {
                var success = await _repository.RemoverAsync(id);
                if(success is not null) return OperationResult<EmpresaDto?>.Success(null);

                return OperationResult<EmpresaDto?>.Failure("Empresa não encontrada.", (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<EmpresaDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
