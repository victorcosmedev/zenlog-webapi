using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Infra.Data.AppData;
using ZenLogAPI.Infra.Data.Repositories;

namespace ZenLogAPI.Test.App;

public class LogEmocionalRepositoryTest
{
    private ApplicationContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    [Trait("Repository", "LogEmocional")]
    public async Task AdicionarAsync_DeveRetornarNull_QuandoColaboradorNaoExiste()
    {
        await using var context = CreateContext();
        var repository = new LogEmocionalRepository(context);

        var novoLogEmocional = new LogEmocionalEntity
        {
            ColaboradorId = int.MaxValue,
            NivelEmocional = NivelEmocional.Mal,
            DescricaoSentimento = "Me sinto estressado",
            FezExercicios = false,
            HorasDescanso = 4,
            LitrosAgua = 1,
            CreatedAt = DateTime.UtcNow
        };

        var logSalvo = await repository.AdicionarAsync(novoLogEmocional);

        Assert.Null(logSalvo);
    }
}
