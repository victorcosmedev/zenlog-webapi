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
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaApplicationService _service;
        public EmpresaController(IEmpresaApplicationService empresaService)
        {
            _service = empresaService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = ApiDoc.AdicionarEmpresaAsyncSummary,
            Description = ApiDoc.AdicionarEmpresaAsyncDescription
        )]
        public async Task<IActionResult> AdicionarAsync([FromBody] EmpresaDto empresaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.AdicionarAsync(empresaDto);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                    new LinkDto { Rel = "get", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                    new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                    new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" },
                    new LinkDto { Rel = "list", Href = Url.Action(nameof(ListarAsync)), Method = "GET" }
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
            Summary = ApiDoc.EditarEmpresaAsyncSummary,
            Description = ApiDoc.EditarEmpresaAsyncDescription
        )]
        public async Task<IActionResult> EditarAsync(int id, [FromBody] EmpresaDto empresaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.EditarAsync(id, empresaDto);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);


                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                    new LinkDto { Rel = "get", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                    new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                    new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" },
                    new LinkDto { Rel = "list", Href = Url.Action(nameof(ListarAsync)), Method = "GET" }
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
            Summary = ApiDoc.RemoverEmpresaAsyncSummary,
            Description = ApiDoc.RemoverEmpresaAsyncDescription
        )]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            try
            {
                var result = await _service.RemoverAsync(id);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value, // Assumindo que o serviço retorna o objeto deletado
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                    new LinkDto { Rel = "list", Href = Url.Action(nameof(ListarAsync)), Method = "GET" }
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
            Summary = ApiDoc.ListarEmpresasAsyncSummary,
            Description = ApiDoc.ListarEmpresasAsyncDescription
        )]
        public async Task<IActionResult> ListarAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _service.ListarAsync(pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if (result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhuma empresa encontrada.");

                var pageResults = BuildPageResultsForListar(result.Value);

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(ListarAsync), new { pageNumber, pageSize }), Method = "GET" },
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

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = ApiDoc.BuscarPorIdEmpresaAsyncSummary,
            Description = ApiDoc.BuscarPorIdEmpresaAsyncDescription
        )]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            try
            {
                var result = await _service.BuscarPorIdAsync(id);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<EmpresaDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "create", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" },
                        new LinkDto { Rel = "list", Href = Url.Action(nameof(ListarAsync)), Method = "GET" }
                    }
                };

                return Ok(hateoas);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Helpers 
        private PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>> BuildPageResultsForListar(PageResultModel<IEnumerable<EmpresaDto>> pageResult)
        {
            var pageResults = new PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>
            {
                Items = pageResult.Items.Select(empresa => new HateoasResponse<EmpresaDto>
                {
                    Data = empresa,
                    Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = empresa.Id }), Method = "GET" },
                    new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = empresa.Id }), Method = "PUT" },
                    new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = empresa.Id }), Method = "DELETE" }
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
}
