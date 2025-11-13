using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;
using ZenLogAPI.Utils.Doc;

namespace ZenLogAPI.Controllers
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

        [HttpPost]
        [SwaggerOperation(
            Summary = ApiDoc.AdicionarLogAsyncSummary,
            Description = ApiDoc.AdicionarLogAsyncDescription
        )]
        public async Task<IActionResult> AdicionarAsync([FromBody] LogEmocionalDto logDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.AdicionarAsync(logDto);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                    new LinkDto { Rel = "get", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                    new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                    new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" },
                    new LinkDto { Rel = "list-by-colaborador", Href = Url.Action(nameof(ListarPorColaboradorAsync), new { colaboradorId = result.Value.ColaboradorId }), Method = "GET" }
                }
                };

                return CreatedAtAction(nameof(BuscarPorIdAsync), new { id = result.Value.Id }, hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = ApiDoc.EditarLogAsyncSummary,
            Description = ApiDoc.EditarLogAsyncDescription
        )]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] LogEmocionalDto logDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.EditarAsync(id, logDto);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);


                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                    new LinkDto { Rel = "get", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                    new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                    new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" },
                    new LinkDto { Rel = "listByColaborador", Href = Url.Action(nameof(ListarPorColaboradorAsync), new { colaboradorId = result.Value.ColaboradorId }), Method = "GET" }
                }
                };

                return Ok(hateoas);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = ApiDoc.RemoverLogAsyncSummary,
            Description = ApiDoc.RemoverLogAsyncDescription
        )]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            try
            {
                var result = await _service.RemoverAsync(id);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value, // Assumindo que o serviço retorna o objeto deletado
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                    new LinkDto { Rel = "list-by-colaborador", Href = Url.Action(nameof(ListarPorColaboradorAsync)), Method = "GET" }
                }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorIdLogAsyncSummary,
            Description = ApiDoc.BuscarPorIdLogAsyncDescription
        )]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            try
            {
                var result = await _service.BuscarPorIdAsync(id);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<LogEmocionalDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" },
                        new LinkDto { Rel = "list-by-colaborador", Href = Url.Action(nameof(ListarPorColaboradorAsync), new { colaboradorId = result.Value.ColaboradorId }), Method = "GET" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = ApiDoc.ListarPorColaboradorAsyncSummary,
            Description = ApiDoc.ListarPorColaboradorAsyncDescription
        )]
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

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(ListarPorColaboradorAsync), new { colaboradorId, pageNumber, pageSize }), Method = "GET" },
                    new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" }
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
            var pageResults = new PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>
            {
                Items = pageResult.Items.Select(log => new HateoasResponse<LogEmocionalDto>
                {
                    Data = log,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = log.Id }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = log.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = log.Id }), Method = "DELETE" },
                        new LinkDto { Rel = "list-by-colaborador", Href = Url.Action(nameof(ListarPorColaboradorAsync), new { colaboradorId = log.ColaboradorId }), Method = "GET" }
                    }
                }),
                TotalItens = pageResult.TotalItens,
                NumeroPagina = pageResult.NumeroPagina,
                TamanhoPagina = pageResult.TamanhoPagina
            };

            return pageResults;
        }
        #endregion
    }
}
