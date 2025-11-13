using Swashbuckle.AspNetCore.Filters;
using ZenLogAPI.Application.DTOs;

namespace ZenLogAPI.Utils.Samples.Colaborador
{
    public class ColaboradorResponseListSample : IExamplesProvider<IEnumerable<ColaboradorDto>>
    {
        public IEnumerable<ColaboradorDto> GetExamples()
        {
            return new List<ColaboradorDto>
            {
                new ColaboradorDto {
                    Id = 0,
                    Username = "novousuario",
                    Email = "novousuario@gmail.com",
                    DataNascimento = new DateTime(1995, 8, 15),
                    NumeroMatricula = "0987654321",
                    Cpf = "10987654321",
                    EmpresaId = 2
                },
                new ColaboradorDto
                {
                    Id = 1,
                    Username = "joaosilva",
                    Email = "joaosilva@gmail.com",
                    DataNascimento = new DateTime(1990, 5, 20),
                    NumeroMatricula = "1234567890",
                    Cpf = "12345678901",
                    EmpresaId = 1
                }
            };
        }
    }
}
