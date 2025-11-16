using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using ZenLogAPI.Application.DTOs.Empresa;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;
using ZenLogAPI.Utils.Doc;
using ZenLogAPI.Utils.Samples.Empresa;

namespace ZenLogAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaApplicationService _service;
        private readonly ILogger<EmpresaController> _logger;
        public EmpresaController(IEmpresaApplicationService empresaService, ILogger<EmpresaController> logger)
        {
            _service = empresaService;
            _logger = logger;
        }

        [HttpPost(Name = "CreateEmpresaV1")]
        [SwaggerOperation(
            Summary = ApiDoc.AdicionarEmpresaAsyncSummary,
            Description = ApiDoc.AdicionarEmpresaAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "Empresa criada com sucesso", typeof(HateoasResponse<EmpresaDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(EmpresaResponseSample))]
        public async Task<IActionResult> AdicionarAsync([FromBody] EmpresaDto empresaDto)
        {
            var traceId = HttpContext.TraceIdentifier;
            
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.AdicionarAsync(empresaDto);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();

                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("CreateEmpresaV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "get", Href = Url.RouteUrl("GetEmpresaByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateEmpresaV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteEmpresaV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "list", Href = Url.RouteUrl("ListEmpresasV1", new { version = apiVersion.ToString() }), Method = "GET" }
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

        [HttpPut("{id}", Name = "UpdateEmpresaV1")]
        [SwaggerOperation(
            Summary = ApiDoc.EditarEmpresaAsyncSummary,
            Description = ApiDoc.EditarEmpresaAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Empresa editada com sucesso", typeof(HateoasResponse<EmpresaDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Empresa não encontrada", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmpresaResponseSample))]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] EmpresaDto empresaDto)
        {
            var traceId = HttpContext.TraceIdentifier;
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.EditarAsync(id, empresaDto);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);


                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("UpdateEmpresaV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "get", Href = Url.RouteUrl("GetEmpresaByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateEmpresaV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteEmpresaV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "list", Href = Url.RouteUrl("ListEmpresasV1", new { version = apiVersion.ToString() }), Method = "GET" }
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

        [HttpDelete("{id}", Name = "DeleteEmpresaV1")]
        [SwaggerOperation(
            Summary = ApiDoc.RemoverEmpresaAsyncSummary,
            Description = ApiDoc.RemoverEmpresaAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Empresa removida com sucesso", typeof(HateoasResponse<EmpresaDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Empresa não encontrada.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmpresaResponseSample))]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            var traceId = HttpContext.TraceIdentifier;
            try
            {
                var result = await _service.RemoverAsync(id);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateEmpresaV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "list", Href = Url.RouteUrl("ListEmpresasV1", new { version = apiVersion.ToString() }), Method = "GET" }
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

        [HttpGet(Name = "ListEmpresasV1")]
        [SwaggerOperation(
            Summary = ApiDoc.ListarEmpresasAsyncSummary,
            Description = ApiDoc.ListarEmpresasAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de empresas paginada", typeof(HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhuma empresa encontrada.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmpresaResponseListSample))]
        public async Task<IActionResult> ListarAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var traceId = HttpContext.TraceIdentifier;
            try
            {
                var result = await _service.ListarAsync(pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if (result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhuma empresa encontrada.");

                var pageResults = BuildPageResultsForListar(result.Value);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("ListEmpresasV1", new { pageNumber, pageSize, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateEmpresaV1", new { version = apiVersion.ToString() }), Method = "POST" }
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

        [HttpGet("{id}", Name = "GetEmpresaByIdV1")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorIdEmpresaAsyncSummary,
            Description = ApiDoc.BuscarPorIdEmpresaAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Empresa encontrada com sucesso", typeof(HateoasResponse<EmpresaDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Empresa não encontrada", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmpresaResponseSample))]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            var traceId = HttpContext.TraceIdentifier;
            try
            {
                var result = await _service.BuscarPorIdAsync(id);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetEmpresaByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateEmpresaV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateEmpresaV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteEmpresaV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "list", Href = Url.RouteUrl("ListEmpresasV1", new { version = apiVersion.ToString() }), Method = "GET" }
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
        private PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>> BuildPageResultsForListar(PageResultModel<IEnumerable<EmpresaDto>> pageResult)
        {
            var apiVersion = HttpContext.GetRequestedApiVersion();

            var pageResults = new PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>
            {
                Items = pageResult.Items.Select(empresa => new HateoasResponse<EmpresaDto>
                {
                    Data = empresa,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetEmpresaByIdV1", new { id = empresa.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateEmpresaV1", new { id = empresa.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteEmpresaV1", new { id = empresa.Id, version = apiVersion.ToString() }), Method = "DELETE" }
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
