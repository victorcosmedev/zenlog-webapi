using Microsoft.AspNetCore.Mvc;
using ZenLogAPI.Application.Interfaces;

namespace ZenLogAPI.Controllers
{
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorApplicationService _colaboradorService;
        public ColaboradorController(IColaboradorApplicationService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }


    }
}
