using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;
using ZenLogAPI.Utils.Doc;
using ZenLogAPI.Utils.Samples.LogEmocional;

namespace ZenLogAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class LogEmocionalController : ControllerBase
    {
        private readonly ILogEmocionalApplicationService _service;

        public LogEmocionalController(ILogEmocionalApplicationService logEmocionalService)
        {
            _service = logEmocionalService;
        }

        [HttpPost(Name = "CreateLogEmocionalV1")]
        [SwaggerOperation(
            Summary = ApiDoc.AdicionarLogAsyncSummary,
            Description = ApiDoc.AdicionarLogAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "Log emocional criado com sucesso", typeof(HateoasResponse<LogEmocionalDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Colaborador associado não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(LogEmocionalResponseSample))]
        public async Task<IActionResult> AdicionarAsync([FromBody] LogEmocionalDto logDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.AdicionarAsync(logDto);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("CreateLogEmocionalV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "get", Href = Url.RouteUrl("GetLogEmocionalByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateLogEmocionalV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteLogEmocionalV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "list-by-colaborador", Href = Url.RouteUrl("ListLogsByColaboradorV1", new { colaboradorId = result.Value.ColaboradorId, version = apiVersion.ToString() }), Method = "GET" }
                    }
                };

                return StatusCode(result.StatusCode, hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateLogEmocionalV1")]
        [SwaggerOperation(
            Summary = ApiDoc.EditarLogAsyncSummary,
            Description = ApiDoc.EditarLogAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Log emocional editado com sucesso", typeof(HateoasResponse<LogEmocionalDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Log emocional ou colaborador associado não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LogEmocionalResponseSample))]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] LogEmocionalDto logDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.EditarAsync(id, logDto);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);


                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("UpdateLogEmocionalV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "get", Href = Url.RouteUrl("GetLogEmocionalByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateLogEmocionalV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteLogEmocionalV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "listByColaborador", Href = Url.RouteUrl("ListLogsByColaboradorV1", new { colaboradorId = result.Value.ColaboradorId, version = apiVersion.ToString() }), Method = "GET" }
                    }
                };

                return Ok(hateoas);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteLogEmocionalV1")]
        [SwaggerOperation(
            Summary = ApiDoc.RemoverLogAsyncSummary,
            Description = ApiDoc.RemoverLogAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Log emocional removido com sucesso", typeof(HateoasResponse<LogEmocionalDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Log emocional não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LogEmocionalResponseSample))]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            try
            {
                var result = await _service.RemoverAsync(id);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateLogEmocionalV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "list-by-colaborador", Href = Url.RouteUrl("ListLogsByColaboradorV1", new { version = apiVersion.ToString() }), Method = "GET" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetLogEmocionalByIdV1")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorIdLogAsyncSummary,
            Description = ApiDoc.BuscarPorIdLogAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Log emocional encontrado com sucesso", typeof(HateoasResponse<LogEmocionalDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Log emocional não encontrado", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LogEmocionalResponseSample))]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            try
            {
                var result = await _service.BuscarPorIdAsync(id);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var apiVersion = HttpContext.GetRequestedApiVersion();
                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetLogEmocionalByIdV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateLogEmocionalV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateLogEmocionalV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteLogEmocionalV1", new { id = result.Value.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "list-by-colaborador", Href = Url.RouteUrl("ListLogsByColaboradorV1", new { colaboradorId = result.Value.ColaboradorId, version = apiVersion.ToString() }), Method = "GET" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet(Name = "ListLogsByColaboradorV1")]
        [SwaggerOperation(
            Summary = ApiDoc.ListarPorColaboradorAsyncSummary,
            Description = ApiDoc.ListarPorColaboradorAsyncDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de logs emocionais paginada", typeof(HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na requisição", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum log encontrado.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor", typeof(string))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LogEmocionalResponseListSample))]
        public async Task<IActionResult> ListarPorColaboradorAsync([FromQuery] int colaboradorId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (colaboradorId <= 0)
            {
                return BadRequest(new { Error = "O parâmetro 'colaboradorId' é obrigatório e deve ser maior que zero." });
            }

            try
            {
                var result = await _service.ListarPorColaboradorAsync(colaboradorId, pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if (result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhum log emocional encontrado para este colaborador.");

                var pageResults = BuildPageResultsForListar(result.Value);

                var apiVersion = HttpContext.GetRequestedApiVersion();

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("ListLogsByColaboradorV1", new { colaboradorId, pageNumber, pageSize, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateLogEmocionalV1", new { version = apiVersion.ToString() }), Method = "POST" }
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Helpers 
        private PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>> BuildPageResultsForListar(PageResultModel<IEnumerable<LogEmocionalDto>> pageResult)
        {
            var apiVersion = HttpContext.GetRequestedApiVersion();

            var pageResults = new PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>
            {
                Items = pageResult.Items.Select(log => new HateoasResponse<LogEmocionalDto>
                {
                    Data = log,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.RouteUrl("GetLogEmocionalByIdV1", new { id = log.Id, version = apiVersion.ToString() }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.RouteUrl("CreateLogEmocionalV1", new { version = apiVersion.ToString() }), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.RouteUrl("UpdateLogEmocionalV1", new { id = log.Id, version = apiVersion.ToString() }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.RouteUrl("DeleteLogEmocionalV1", new { id = log.Id, version = apiVersion.ToString() }), Method = "DELETE" },
                        new LinkDto { Rel = "list-by-colaborador", Href = Url.RouteUrl("ListLogsByColaboradorV1", new { colaboradorId = log.ColaboradorId, version = apiVersion.ToString() }), Method = "GET" }
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
