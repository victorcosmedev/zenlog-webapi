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
    public class ColaboradorApplicationService : IColaboradorApplicationService
    {
        private readonly IColaboradorRepository _repository;
        public ColaboradorApplicationService(IColaboradorRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<ColaboradorDto?>> AdicionarAsync(ColaboradorDto colaboradorDto)
        {
            try
            {
                var entity = colaboradorDto.ToEntity();
                var colaborador = await _repository.AdicionarAsync(entity);
                if (colaborador is null)
                    return OperationResult<ColaboradorDto?>.Failure("Empresa associada não encontrada.", (int)HttpStatusCode.NotFound);

                var dto = colaborador.ToDto();
                return OperationResult<ColaboradorDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ColaboradorDto?>> BuscarPorCpfAsync(string cpf)
        {
            try
            {
                var entity = await _repository.BuscarPorCpfAsync(cpf);
                if (entity is null)
                    return OperationResult<ColaboradorDto?>.Failure("Colaborador não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = entity.ToDto();
                return OperationResult<ColaboradorDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ColaboradorDto?>> BuscarPorEmailAsync(string email)
        {
            try
            {
                var entity = await _repository.BuscarPorEmailAsync(email);
                if (entity is null)
                    return OperationResult<ColaboradorDto?>.Failure("Colaborador não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = entity.ToDto();
                return OperationResult<ColaboradorDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ColaboradorDto?>> BuscarPorIdAsync(int id)
        {
            try
            {
                var entity = await _repository.BuscarPorIdAsync(id);
                if (entity is null)
                    return OperationResult<ColaboradorDto?>.Failure("Colaborador não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = entity.ToDto();
                return OperationResult<ColaboradorDto?>.Success(dto);

            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ColaboradorDto?>> BuscarPorMatriculaAsync(string numeroMatricula)
        {
            try
            {
                var entity = await _repository.BuscarPorMatriculaAsync(numeroMatricula);
                if (entity is null)
                    return OperationResult<ColaboradorDto?>.Failure("Colaborador não encontrado.", (int)HttpStatusCode.NotFound);

                var dto = entity.ToDto();
                return OperationResult<ColaboradorDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ColaboradorDto?>> EditarAsync(int id, ColaboradorDto colaboradorDto)
        {
            try
            {
                ColaboradorEntity? colaborador = colaboradorDto.ToEntity();

                var success = await _repository.EditarAsync(id, colaborador);

                if (success is null)
                {
                    return OperationResult<ColaboradorDto?>.Failure("Colaborador ou empresa associada não encontrado.", (int)HttpStatusCode.NotFound);
                }

                var dto = success.ToDto();
                return OperationResult<ColaboradorDto?>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>> ListarAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pageResult = await _repository.ListarAsync(pageNumber, pageSize);

                if (pageResult.Items is null)
                {
                    return OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>.Failure("Nenhum colaborador encontrado.", (int)HttpStatusCode.NotFound);
                }

                var dtos = pageResult.Items.Select(x => x.ToDto());

                var pageResultDto = new PageResultModel<IEnumerable<ColaboradorDto>>
                {
                    Items = dtos,
                    TotalItens = pageResult.TotalItens,
                    NumeroPagina = pageResult.NumeroPagina,
                    TamanhoPagina = pageResult.TamanhoPagina
                };

                return OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>.Success(pageResultDto);
            }
            catch (Exception ex)
            {
                return OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>> ListarPorEmpresaAsync(int empresaId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pageResult = await _repository.ListarPorEmpresaAsync(empresaId, pageNumber, pageSize);

                if(pageResult.Items is null)
                {
                    return OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>.Failure("Nenhum colaborador encontrado para a empresa especificada.", (int)HttpStatusCode.NotFound);
                }

                var dtos = pageResult.Items.Select(x => x.ToDto());

                var pageResultDto = new PageResultModel<IEnumerable<ColaboradorDto>>
                {
                    Items = dtos,
                    TotalItens = pageResult.TotalItens,
                    NumeroPagina = pageResult.NumeroPagina,
                    TamanhoPagina = pageResult.TamanhoPagina
                };

                return OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>.Success(pageResultDto);
            }
            catch (Exception ex)
            {
                return OperationResult<PageResultModel<IEnumerable<ColaboradorDto>>>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<OperationResult<ColaboradorDto?>> RemoverAsync(int id)
        {
            try
            {
                var success = await _repository.RemoverAsync(id);
                if(success is not null) return OperationResult<ColaboradorDto?>.Success(null);

                return OperationResult<ColaboradorDto?>.Failure("Colaborador não encontrado para remoção.", (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<ColaboradorDto?>.Failure($"Erro: {ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
