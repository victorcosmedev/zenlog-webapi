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
    public class LogEmocionalApplicationService : ILogEmocionalApplicationService
    {
        private readonly ILogEmocionalRepository _repository;
        public LogEmocionalApplicationService(ILogEmocionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<LogEmocionalDto?>> AdicionarAsync(LogEmocionalDto logDto)
        {
            try
            {
                var entity = logDto.ToEntity();
                var logEmocional = await _repository.AdicionarAsync(entity);
                if (logEmocional is null)
                    return OperationResult<LogEmocionalDto?>.Failure("Colaborador associado não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = logEmocional.ToDto();
                return OperationResult<LogEmocionalDto?>.Success(dto, (int)HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return OperationResult<LogEmocionalDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LogEmocionalDto?>> BuscarPorIdAsync(int id)
        {
            try
            {
                var entity = await _repository.BuscarPorIdAsync(id);
                if (entity is null)
                    return OperationResult<LogEmocionalDto?>.Failure("Log emocional não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = entity.ToDto();
                return OperationResult<LogEmocionalDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<LogEmocionalDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LogEmocionalDto?>> EditarAsync(int id, LogEmocionalDto logDto)
        {
            try
            {
                LogEmocionalEntity? logEmocional = logDto.ToEntity();

                var success = await _repository.EditarAsync(id, logEmocional);

                if (success is null)
                    return OperationResult<LogEmocionalDto?>.Failure("Log emocional ou colaborador associado não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = success.ToDto();
                return OperationResult<LogEmocionalDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<LogEmocionalDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<LogEmocionalDto>>>> ListarPorColaboradorAsync(int colaboradorId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pageResult = await _repository.ListarPorColaboradorAsync(colaboradorId, pageNumber, pageSize);

                if(pageResult.Items is null)
                {
                    return OperationResult<PageResultModel<IEnumerable<LogEmocionalDto>>>.Failure("Nenhum log encontrado.", (int)HttpStatusCode.NotFound);
                }

                var dtos = pageResult.Items.Select(x => x.ToDto());

                var pageResultDto = new PageResultModel<IEnumerable<LogEmocionalDto>>
                {
                    Items = dtos,
                    TotalItens = pageResult.TotalItens,
                    NumeroPagina = pageResult.NumeroPagina,
                    TamanhoPagina = pageResult.TamanhoPagina
                };

                return OperationResult<PageResultModel<IEnumerable<LogEmocionalDto>>>.Success(pageResultDto);
            }
            catch (Exception ex)
            {
                return OperationResult<PageResultModel<IEnumerable<LogEmocionalDto>>>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<LogEmocionalDto?>> RemoverAsync(int id)
        {
            try
            {
                var success = await _repository.RemoverAsync(id);

                if(success is not null) 
                    return OperationResult<LogEmocionalDto?>.Success(success.ToDto());

                return OperationResult<LogEmocionalDto?>.Failure("Log emocional não encontrado.", (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<LogEmocionalDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<IEnumerable<LogEmocionalDto>?>> ListarTodosAsync()
        {
            try
            {
                var logsEmocionais = await _repository.ListarTodosAsync();
                if (logsEmocionais is null || !logsEmocionais.Any())
                    return OperationResult<IEnumerable<LogEmocionalDto>?>.Failure("Nenhum log emocional encontrado.", (int)HttpStatusCode.NotFound);

                var dtos = logsEmocionais.Select(log => log.ToDto());

                return OperationResult<IEnumerable<LogEmocionalDto>?>.Success(dtos);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<LogEmocionalDto>?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
