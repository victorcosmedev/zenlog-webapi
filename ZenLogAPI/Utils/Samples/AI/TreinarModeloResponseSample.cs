using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs.AI;

namespace ZenLogAPI.Utils.Samples.AI
{
    public class TreinarModeloResponseSample : IExamplesProvider<MessageResponseDto>
    {
        public MessageResponseDto GetExamples()
        {
            return new MessageResponseDto
            {
                Message = "Modelo treinado e salvo com sucesso."
            };
        }
    }
}
