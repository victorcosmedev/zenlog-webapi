using Microsoft.EntityFrameworkCore;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Infra.Data.AppData;
using ZenLogAPI.Infra.Data.Repositories;

namespace ZenLogAPI.Test.App;

public class EmpresaRepositoryTest
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
    [Trait("Repository", "Empresa")]
    public async Task AdicionarAsync_DeveAdicionarEmpresaNoBanco()
    {
        await using var context = CreateContext();
        var repository = new EmpresaRepository(context);

        var novaEmpresa = new EmpresaEntity
        {
            RazaoSocial = "Empresa XPTO",
            Setor = SetorEmpresa.Financeiro
        };

        var empresaSalva = await repository.AdicionarAsync(novaEmpresa);

        Assert.NotNull(empresaSalva);
        Assert.True(empresaSalva.Id > 0);
        Assert.Equal("Empresa XPTO", empresaSalva.RazaoSocial);

        var empresaDoBanco = await context.Empresas.FindAsync(empresaSalva.Id);
        Assert.NotNull(empresaDoBanco);
    }
}
