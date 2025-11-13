using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Utils.Samples.Empresa
{
    public class EmpresaResponseSample : IExamplesProvider<EmpresaDto>
    {
        public EmpresaDto GetExamples()
        {
            return new EmpresaDto
            {
                Id = 1,
                RazaoSocial = "Empresa de Varejo XYZ",
                Setor = SetorEmpresa.Varejo
            };
        }
    }
}
