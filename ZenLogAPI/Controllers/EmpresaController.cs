using Microsoft.AspNetCore.Mvc;
using ZenLogAPI.Application.Interfaces;

namespace ZenLogAPI.Controllers
{
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaApplicationService _empresaService;
        public EmpresaController(IEmpresaApplicationService empresaService)
        {
            _empresaService = empresaService;
        }
    }
}
