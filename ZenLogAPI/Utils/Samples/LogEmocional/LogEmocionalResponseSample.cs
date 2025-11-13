using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Utils.Samples.LogEmocional
{
    public class LogEmocionalResponseSample : IExamplesProvider<LogEmocionalDto>
    {
        public LogEmocionalDto GetExamples()
        {
            return new LogEmocionalDto
            {
                Id = 1,
                NivelEmocional = NivelEmocional.Mal,
                DescricaoSentimento = "Muitas reuniões, cansaço.",
                FezExercicios = false,
                HorasDescanso = 6,
                CreatedAt = new DateTime(2025, 11, 11, 17, 30, 0),
                ColaboradorId = 1
            };
        }
    }
}
