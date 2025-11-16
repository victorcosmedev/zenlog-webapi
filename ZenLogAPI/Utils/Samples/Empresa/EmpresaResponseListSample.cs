using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs.Empresa;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Utils.Samples.Empresa
{
    public class EmpresaResponseListSample : IExamplesProvider<HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>>>
    {
        public HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>> GetExamples()
        {
            var empresasDto = new List<EmpresaDto>
            {
                new EmpresaDto
                {
                    Id = 1,
                    RazaoSocial = "Empresa de Varejo XYZ",
                    Setor = SetorEmpresa.Varejo
                },
                new EmpresaDto
                {
                    Id = 2,
                    RazaoSocial = "Banco Digital Zen",
                    Setor = SetorEmpresa.Financeiro
                }
            };

            var empresasHateoas = empresasDto.Select(empresa => new HateoasResponse<EmpresaDto>
            {
                Data = empresa,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = $"/api/Empresa/{empresa.Id}", Method = "GET" },
                    new LinkDto { Rel = "create", Href = $"/api/Empresa", Method = "POST" },
                    new LinkDto { Rel = "update", Href = $"/api/Empresa/{empresa.Id}", Method = "PUT" },
                    new LinkDto { Rel = "delete", Href = $"/api/Empresa/{empresa.Id}", Method = "DELETE" },
                    new LinkDto { Rel = "list", Href = "/api/Empresa", Method = "GET" }
                }
            });

            var pageResult = new PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>
            {
                Items = empresasHateoas,
                TotalItens = 2,
                NumeroPagina = 1,
                TamanhoPagina = 10
            };

            return new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<EmpresaDto>>>>
            {
                Data = pageResult,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = "/api/Empresa?pageNumber=1&pageSize=10", Method = "GET" },
                    new LinkDto { Rel = "create", Href = "/api/Empresa", Method = "POST" }
                }
            };
        }
    }
}
