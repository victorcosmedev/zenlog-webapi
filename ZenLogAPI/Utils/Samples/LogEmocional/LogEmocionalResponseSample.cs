using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.Hateoas;

namespace ZenLogAPI.Utils.Samples.LogEmocional
{
    public class LogEmocionalResponseSample : IExamplesProvider<HateoasResponse<LogEmocionalDto>>
    {
        public HateoasResponse<LogEmocionalDto> GetExamples()
        {
            var log = new LogEmocionalDto
            {
                Id = 1,
                NivelEmocional = NivelEmocional.Mal,
                DescricaoSentimento = "Muitas reuniões, cansaço.",
                FezExercicios = false,
                HorasDescanso = 6,
                CreatedAt = new DateTime(2025, 11, 11, 17, 30, 0),
                ColaboradorId = 1
            };

            return new HateoasResponse<LogEmocionalDto>
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
            };
        }
    }
}
