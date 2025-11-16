using Swashbuckle.AspNetCore.Filters;
using static ZenLogAPI.Controllers.v1.AIController;

namespace ZenLogAPI.Utils.Samples.AI
{
    public class PredicaoRequestSample : IExamplesProvider<LogEmocionalTrainingData>
    {
        public LogEmocionalTrainingData GetExamples()
        {
            return new LogEmocionalTrainingData
            {
                FezExercicios = 1,
                HorasDescanso = 8,
                LitrosAgua = 3,
                NivelEmocional = 0
            };
        }
    }
}
