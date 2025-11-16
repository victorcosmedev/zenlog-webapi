using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs.Empresa;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.Hateoas;

namespace ZenLogAPI.Utils.Samples.Empresa
{
    public class EmpresaResponseSample : IExamplesProvider<HateoasResponse<EmpresaDto>>
    {
        public HateoasResponse<EmpresaDto> GetExamples()
        {
            var empresa = new EmpresaDto
            {
                Id = 1,
                RazaoSocial = "Empresa de Varejo XYZ",
                Setor = SetorEmpresa.Varejo
            };

            return new HateoasResponse<EmpresaDto>
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
            };
        }
    }
}
