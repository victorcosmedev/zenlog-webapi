using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Utils.Samples.Empresa
{
    public class EmpresaResponseListSample : IExamplesProvider<IEnumerable<EmpresaDto>>
    {
        public IEnumerable<EmpresaDto> GetExamples()
        {
            return new List<EmpresaDto>
        {
            new EmpresaDto
            {
                Id = 1,
                RazaoSocial = "Empresa de Varejo XYZ",
                Setor = SetorEmpresa.Varejo
            },
            new EmpresaDto
            {
                Id = 2,
                RazaoSocial = "Banco Digital Zen",
                Setor = SetorEmpresa.Financeiro
            }
        };
        }
    }
}
