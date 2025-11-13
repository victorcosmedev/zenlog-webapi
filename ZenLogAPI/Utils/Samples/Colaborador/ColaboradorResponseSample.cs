using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;

namespace ZenLogAPI.Utils.Samples.Colaborador
{
    public class ColaboradorResponseSample : IExamplesProvider<ColaboradorDto>
    {
        public ColaboradorDto GetExamples()
        {
            return new ColaboradorDto
            {
                Id = 1,
                Username = "joaosilva",
                Email = "joaosilva@gmail.com",
                DataNascimento = new DateTime(1990, 5, 20),
                NumeroMatricula = "1234567890",
                Cpf = "12345678901",
                EmpresaId = 1
            };
        }
    }
}
