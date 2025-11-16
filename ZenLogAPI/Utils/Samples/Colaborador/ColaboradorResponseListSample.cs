using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs.Colaborador;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Utils.Samples.Colaborador
{
    public class ColaboradorResponseListSample : IExamplesProvider<HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>>
    {
        public HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>> GetExamples()
        {
            var colaboradoresDto = new List<ColaboradorDto>
            {
                new ColaboradorDto {
                    Id = 1,
                    Username = "joaosilva",
                    Email = "joaosilva@gmail.com",
                    DataNascimento = new DateTime(1990, 5, 20),
                    NumeroMatricula = "1234567890",
                    Cpf = "12345678901",
                    EmpresaId = 1
                },
                new ColaboradorDto {
                    Id = 2,
                    Username = "mariasouza",
                    Email = "mariasouza@gmail.com",
                    DataNascimento = new DateTime(1995, 8, 15),
                    NumeroMatricula = "0987654321",
                    Cpf = "10987654321",
                    EmpresaId = 1
                }
            };

            var colaboradoresHateoas = colaboradoresDto.Select(c => new HateoasResponse<ColaboradorDto>
            {
                Data = c,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = $"/api/Colaborador/{c.Id}", Method = "GET" },
                    new LinkDto { Rel = "update", Href = $"/api/Colaborador/{c.Id}", Method = "PUT" }
                }
            });

            var pageResult = new PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>
            {
                Items = colaboradoresHateoas,
                TotalItens = 2,
                NumeroPagina = 1,
                TamanhoPagina = 10
            };

            return new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<ColaboradorDto>>>>
            {
                Data = pageResult,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = "/api/Colaborador?pageNumber=1&pageSize=10", Method = "GET" },
                    new LinkDto { Rel = "create", Href = "/api/Colaborador", Method = "POST" }
                }
            };
        }
    }
}
