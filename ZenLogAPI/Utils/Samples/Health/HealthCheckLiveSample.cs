using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs.HealthChecks;

namespace ZenLogAPI.Utils.Samples.Health
{
    public class HealthCheckLiveSample : IExamplesProvider<HealthCheckResponseDto>
    {
        public HealthCheckResponseDto GetExamples()
        {
            return new HealthCheckResponseDto
            {
                Status = "Healthy",
                Checks = new List<HealthCheckItemDto>
                {
                    new HealthCheckItemDto
                    {
                        Name = "self",
                        Status = "Healthy",
                        Description = null,
                        Error = null
                    }
                }
            };
        }
    }
}
