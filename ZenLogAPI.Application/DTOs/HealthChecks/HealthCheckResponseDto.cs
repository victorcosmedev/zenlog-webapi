using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Application.DTOs.HealthChecks
{
    public class HealthCheckResponseDto
    {
        public string Status { get; set; }
        public IEnumerable<HealthCheckItemDto> Checks { get; set; }
    }
}
