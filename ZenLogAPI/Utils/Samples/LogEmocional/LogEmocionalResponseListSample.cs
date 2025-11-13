using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Utils.Samples.LogEmocional
{
    public class LogEmocionalResponseListSample : IExamplesProvider<IEnumerable<LogEmocionalDto>>
    {
        public IEnumerable<LogEmocionalDto> GetExamples()
        {
            return new List<LogEmocionalDto>
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
                ColaboradorId = 2
            }
        };
        }
    }
}
