using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs.Colaborador;
using ZenLogAPI.Domain.Models.Hateoas;

namespace ZenLogAPI.Utils.Samples.Colaborador
{
    public class ColaboradorResponseSample : IExamplesProvider<HateoasResponse<ColaboradorDto>>
    {
        public HateoasResponse<ColaboradorDto> GetExamples()
        {
            var colaborador = new ColaboradorDto
            {
                Id = 1,
                Username = "joaosilva",
                Email = "joaosilva@gmail.com",
                DataNascimento = new DateTime(1990, 5, 20),
                NumeroMatricula = "1234567890",
                Cpf = "12345678901",
                EmpresaId = 1
            };

            return new HateoasResponse<ColaboradorDto>
            {
                Data = colaborador,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = $"/api/colaborador/{colaborador.Id}", Method = "GET" },
                    new LinkDto { Rel = "create", Href = $"/api/colaborador", Method = "POST" },
                    new LinkDto { Rel = "update", Href = $"/api/colaborador/{colaborador.Id}", Method = "PUT" },
                    new LinkDto { Rel = "delete", Href = $"/api/colaborador/{colaborador.Id}", Method = "DELETE" },
                    new LinkDto { Rel = "list-by-empresa", Href = $"/api/colaborador/por-empresa?empresaId={colaborador.EmpresaId}", Method = "GET" }
                }
            };
        }
    }
}
