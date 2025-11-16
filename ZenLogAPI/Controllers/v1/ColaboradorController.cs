using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.Contracts;
using System.Net;
using ZenLogAPI.Application.DTOs.Colaborador;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;
using ZenLogAPI.Utils.Doc;
using ZenLogAPI.Utils.Samples.Colaborador;

namespace ZenLogAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorApplicationService _service;
        private readonly ILogger<ColaboradorController> _logger;

        public ColaboradorController(IColaboradorApplicationService colaboradorService, ILogger<ColaboradorController> logger)
        {
            _service = colaboradorService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateColaboradorV1")]
        [SwaggerOperation(
            Summary = ApiDoc.AdicionarAsyncSummary,
            Description = ApiDoc.AdicionarAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "Colaborador criado com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Empresa associada não encontrada", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Empresa associada não encontrada", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> AdicionarAsync([FromBody] ColaboradorDto colaboradorDto)
        {
            var traceId = HttpContext.TraceIdentifier;

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.AdicionarAsync(colaboradorDto);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "get", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = result.Value.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = result.Value.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = result.Value.NumeroMatricula, version = apiVersion.ToString()}), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                };

                return StatusCode(result.StatusCode, hateoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateColaboradorV1")]
        [SwaggerOperation(
            Summary = ApiDoc.EditarAsyncSummary,
            Description = ApiDoc.EditarAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Colaborador editado com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador ou empresa associada não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] ColaboradorDto colaboradorDto)
        {
            var traceId = HttpContext.TraceIdentifier;

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.EditarAsync(id, colaboradorDto);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);


                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "get", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = result.Value.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = result.Value.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = result.Value.NumeroMatricula, version = apiVersion.ToString()}), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                };

                return Ok(hateoas);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteColaboradorV1")]
        [SwaggerOperation(
            Summary = ApiDoc.RemoverAsyncSummary,
            Description = ApiDoc.RemoverAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Colaborador removido com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador não encontrado para remoção.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.RemoverAsync(id);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "list", Href = Url.RouteUrl("ListColaboradoresV1", new { version = apiVersion.ToString() }), Method = "GET" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet(Name = "ListColaboradoresV1")]
        [SwaggerOperation(
            Summary = ApiDoc.ListarAsyncSummary,
            Description = ApiDoc.ListarAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de colaboradores paginada", typeof(HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum colaborador encontrado.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseListSample))]
        public async Task<IActionResult> ListarAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.ListarAsync(pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if(result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhum colaborador encontrado.");

                var pageResults = BuildPageResultsForListarTodos(result.Value);

                var apiVersion = HttpContext.GetRequestedApiVersion();

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("ListColaboradoresV1", new { pageNumber, pageSize, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" }
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("por-empresa", Name = "ListColaboradoresByEmpresaV1")]
        [SwaggerOperation(
            Summary = ApiDoc.ListarPorEmpresaSummary,
            Description = ApiDoc.ListarPorEmpresaDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de colaboradores paginada por empresa", typeof(HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum colaborador encontrado para a empresa especificada", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseListSample))]
        public async Task<IActionResult> ListarPorEmpresaAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int empresaId = 0)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.ListarPorEmpresaAsync(empresaId, pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if (result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhum colaborador encontrado para a empresa informada.");

                var pageResults = BuildPageResultsForListarTodos(result.Value);

                var apiVersion = HttpContext.GetRequestedApiVersion();

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("ListColaboradoresByEmpresaV1", new { pageNumber, pageSize, empresaId, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" }
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetColaboradorByIdV1")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorIdAsyncSummary,
            Description = ApiDoc.BuscarPorIdAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Colaborador encontrado com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.BuscarPorIdAsync(id);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = result.Value.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = result.Value.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = result.Value.NumeroMatricula, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("por-email", Name = "GetColaboradorByEmailV1")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorEmailAsyncSummary,
            Description = ApiDoc.BuscarPorEmailAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Colaborador encontrado com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> BuscarPorEmailAsync([FromQuery] string email)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.BuscarPorEmailAsync(email);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = result.Value.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = result.Value.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getById", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = result.Value.NumeroMatricula, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("por-cpf", Name = "GetColaboradorByCpfV1")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorCpfAsyncSummary,
            Description = ApiDoc.BuscarPorCpfAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Colaborador encontrado com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> BuscarPorCpfAsync([FromQuery] string cpf)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.BuscarPorCpfAsync(cpf);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = result.Value.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getById", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = result.Value.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = result.Value.NumeroMatricula, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("por-matricula", Name = "GetColaboradorByMatriculaV1")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorMatriculaAsyncSummary,
            Description = ApiDoc.BuscarPorMatriculaAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Colaborador encontrado com sucesso", typeof(HateoasResponse<ColaboradorDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ColaboradorResponseSample))]
        public async Task<IActionResult> BuscarPorMatriculaAsync([FromQuery] string numeroMatricula)
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _service.BuscarPorMatriculaAsync(numeroMatricula);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = result.Value.NumeroMatricula, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getById", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = result.Value.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = result.Value.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateColaboradorV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Helpers 
        private PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>> BuildPageResultsForListarTodos(PageResultModel<IEnumerable<ColaboradorDto>> pageResult)
        {
            var apiVersion = HttpContext.GetRequestedApiVersion();
            var pageResults = new PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>
            {
                Items = pageResult.Items.Select(c => new HateoasResponse<ColaboradorDto>
                {
                    Data = c,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetColaboradorByIdV1", new { id = c.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.RouteUrl("GetColaboradorByEmailV1", new { email = c.Email, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.RouteUrl("GetColaboradorByCpfV1", new { cpf = c.Cpf, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.RouteUrl("GetColaboradorByMatriculaV1", new { numeroMatricula = c.NumeroMatricula, version = apiVersion.ToString()}), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateColaboradorV1", new { id = c.Id , version = apiVersion.ToString()}), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteColaboradorV1", new { id = c.Id, version = apiVersion.ToString() }), Method = "DELETE" }
                    }
                }).ToList(),
                TotalItens = pageResult.TotalItens,
                NumeroPagina = pageResult.NumeroPagina,
                TamanhoPagina = pageResult.TamanhoPagina
            };

            return pageResults;
        }
        #endregion
    }
}
