using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.Hateoas;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Utils.Samples.LogEmocional
{
    public class LogEmocionalResponseListSample : IExamplesProvider<HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>>>
    {
        public HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>> GetExamples()
        {
            var logsDto = new List<LogEmocionalDto>
            {
                new LogEmocionalDto
                {
                    Id = 1,
                    NivelEmocional = NivelEmocional.Mal,
                    DescricaoSentimento = "Muitas reuniões, cansaço.",
                    FezExercicios = false,
                    HorasDescanso = 6,
                    CreatedAt = new DateTime(2025, 11, 11, 17, 30, 0),
                    ColaboradorId = 1
                },
                new LogEmocionalDto
                {
                    Id = 2,
                    NivelEmocional = NivelEmocional.Excelente,
                    DescricaoSentimento = "Consegui fechar o projeto!",
                    FezExercicios = true,
                    HorasDescanso = 7,
                    CreatedAt = new DateTime(2025, 11, 10, 18, 0, 0),
                    ColaboradorId = 1
                }
            };

            var logsHateoas = logsDto.Select(log => new HateoasResponse<LogEmocionalDto>
            {
                Data = log,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = $"/api/LogEmocional/{log.Id}", Method = "GET" },
                    new LinkDto { Rel = "create", Href = $"/api/LogEmocional", Method = "POST" },
                    new LinkDto { Rel = "update", Href = $"/api/LogEmocional/{log.Id}", Method = "PUT" },
                    new LinkDto { Rel = "delete", Href = $"/api/LogEmocional/{log.Id}", Method = "DELETE" },
                    new LinkDto { Rel = "list-by-colaborador", Href = $"/api/LogEmocional?colaboradorId={log.ColaboradorId}", Method = "GET" }
                }
            });

            var pageResult = new PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>
            {
                Items = logsHateoas,
                TotalItens = 2,
                NumeroPagina = 1,
                TamanhoPagina = 10
            };

            return new HateoasResponse<PageResultModel<IEnumerable<HateoasResponse<LogEmocionalDto>>>>
            {
                Data = pageResult,
                Links = new List<LinkDto>
                {
                    new LinkDto { Rel = "self", Href = "/api/LogEmocional?colaboradorId=1&pageNumber=1&pageSize=10", Method = "GET" },
                    new LinkDto { Rel = "create", Href = "/api/LogEmocional", Method = "POST" }
                }
            };
        }
    }
}
