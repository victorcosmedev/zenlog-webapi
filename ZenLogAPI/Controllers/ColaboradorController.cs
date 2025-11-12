using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Net;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Controllers
{
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorApplicationService _service;
        public ColaboradorController(IColaboradorApplicationService colaboradorService)
        {
            _service = colaboradorService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] ColaboradorDto colaboradorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var result = await _service.AdicionarAsync(colaboradorDto);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(AdicionarAsync)), Method = "POST" },
                        new LinkDto { Rel = "get", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" }
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
        public async Task<IActionResult> EditarAsync(int id, [FromBody] ColaboradorDto colaboradorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.EditarAsync(id, colaboradorDto);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.BadRequest, result.Error);

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            try
            {
                var result = await _service.RemoverAsync(id);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _service.ListarAsync(pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if(result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhum colaborador encontrado.");

                var pageResults = BuildPageResultsForListarTodos(result.Value);

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>
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

        [HttpGet]
        public async Task<IActionResult> ListarPorEmpresaAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int empresaId = 0)
        {
            try
            {
                var result = await _service.ListarPorEmpresaAsync(empresaId, pageNumber, pageSize);
                if (result.IsSuccess == false) return StatusCode(result.StatusCode, result.Error);

                if (result.Value?.Items == null || (!result.Value?.Items.Any() ?? false))
                    return StatusCode((int)HttpStatusCode.NotFound, "Nenhum colaborador encontrado para a empresa informada.");

                var pageResults = BuildPageResultsForListarTodos(result.Value);

                var response = new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>
                {
                    Data = pageResults,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(ListarPorEmpresaAsync), new { pageNumber, pageSize, empresaId }), Method = "GET" },
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
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            try
            {
                var result = await _service.BuscarPorIdAsync(id);

                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.Action(nameof(BuscarPorEmailAsync), new { email = result.Value.Email }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.Action(nameof(BuscarPorCpfAsync), new { cpf = result.Value.Cpf }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.Action(nameof(BuscarPorMatriculaAsync), new { matricula = result.Value.NumeroMatricula }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" }
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
        public async Task<IActionResult> BuscarPorEmailAsync([FromQuery] string email)
        {
            try
            {
                var result = await _service.BuscarPorEmailAsync(email);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorEmailAsync), new { email = result.Value.Email }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.Action(nameof(BuscarPorCpfAsync), new { cpf = result.Value.Cpf }), Method = "GET" },
                        new LinkDto { Rel = "getById", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.Action(nameof(BuscarPorMatriculaAsync), new { matricula = result.Value.NumeroMatricula }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" }
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
        public async Task<IActionResult> BuscarPorCpfAsync([FromQuery] string cpf)
        {
            try
            {
                var result = await _service.BuscarPorCpfAsync(cpf);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);

                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorCpfAsync), new { cpf = result.Value.Cpf }), Method = "GET" },
                        new LinkDto { Rel = "getById", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.Action(nameof(BuscarPorEmailAsync), new { email = result.Value.Email }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.Action(nameof(BuscarPorMatriculaAsync), new { matricula = result.Value.NumeroMatricula }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" }
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
        public async Task<IActionResult> BuscarPorMatriculaAsync([FromQuery] string numeroMatricula)
        {
            try
            {
                var result = await _service.BuscarPorMatriculaAsync(numeroMatricula);
                if (result.IsSuccess == false) return StatusCode((int)HttpStatusCode.NotFound, result.Error);
                
                var hateoas = new HateoasResponse<ColaboradorDto>
                {
                    Data = result.Value,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorMatriculaAsync), new { numeroMatricula = result.Value.NumeroMatricula }), Method = "GET" },
                        new LinkDto { Rel = "getById", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = result.Value.Id }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.Action(nameof(BuscarPorEmailAsync), new { email = result.Value.Email }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.Action(nameof(BuscarPorCpfAsync), new { cpf = result.Value.Cpf }), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = result.Value.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = result.Value.Id }), Method = "DELETE" }
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
        private PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>> BuildPageResultsForListarTodos(PageResultModel<IEnumerable<ColaboradorDto>> pageResult)
        {
            var pageResults = new PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>
            {
                Items = pageResult.Items.Select(c => new HateoasResponse<ColaboradorDto>
                {
                    Data = c,
                    Links = new List<LinkDto>
                    {
                        new LinkDto { Rel = "self", Href = Url.Action(nameof(BuscarPorIdAsync), new { id = c.Id }), Method = "GET" },
                        new LinkDto { Rel = "getByEmail", Href = Url.Action(nameof(BuscarPorEmailAsync), new { id = c.Email }), Method = "GET" },
                        new LinkDto { Rel = "getByCpf", Href = Url.Action(nameof(BuscarPorCpfAsync), new { id = c.Cpf }), Method = "GET" },
                        new LinkDto { Rel = "getByMatricula", Href = Url.Action(nameof(BuscarPorMatriculaAsync), new { id = c.NumeroMatricula}), Method = "GET" },
                        new LinkDto { Rel = "update", Href = Url.Action(nameof(EditarAsync), new { id = c.Id }), Method = "PUT" },
                        new LinkDto { Rel = "delete", Href = Url.Action(nameof(RemoverAsync), new { id = c.Id }), Method = "DELETE" }
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
