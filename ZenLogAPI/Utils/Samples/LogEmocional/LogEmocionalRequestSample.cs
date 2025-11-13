using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Utils.Samples.LogEmocional
{
    public class LogEmocionalRequestSample : IExamplesProvider<LogEmocionalDto>
    {
        public LogEmocionalDto GetExamples()
        {
            return new LogEmocionalDto
            {
                Id = 0,
                NivelEmocional = NivelEmocional.Bem,
                DescricaoSentimento = "Dia foi produtivo.",
                FezExercicios = true,
                HorasDescanso = 8,
                CreatedAt = DateTime.Now.Date,
                ColaboradorId = 1
            };
        }
    }
}
