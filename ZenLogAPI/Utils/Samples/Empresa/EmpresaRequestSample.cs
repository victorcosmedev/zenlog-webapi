using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Utils.Samples.Empresa
{
    public class EmpresaRequestSample : IExamplesProvider<EmpresaDto>
    {
        public EmpresaDto GetExamples()
        {
            return new EmpresaDto
            {
                Id = 0,
                RazaoSocial = "Nova Empresa Tech SA",
                Setor = SetorEmpresa.Tecnologia
            };
        }
    }
}
