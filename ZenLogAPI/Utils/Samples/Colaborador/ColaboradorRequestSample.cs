using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;

namespace ZenLogAPI.Utils.Samples.Colaborador
{
    public class ColaboradorRequestSample : IExamplesProvider<ColaboradorDto>
    {
        public ColaboradorDto GetExamples()
        {
            return new ColaboradorDto
            {
                Id = 0,
                Username = "novousuario",
                Email = "novousuario@gmail.com",
                DataNascimento = new DateTime(1995, 8, 15),
                NumeroMatricula = "0987654321",
                Cpf = "10987654321",
                EmpresaId = 2
            };
        }
    }
}
