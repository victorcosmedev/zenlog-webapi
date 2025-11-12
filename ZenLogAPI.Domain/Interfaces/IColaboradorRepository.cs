using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Models.PageResultModel;

namespace ZenLogAPI.Domain.Interfaces
{
    public interface IColaboradorRepository
    {
        Task<ColaboradorEntity?> BuscarPorIdAsync(int id);
        Task<ColaboradorEntity?> BuscarPorEmailAsync(string email);
        Task<ColaboradorEntity?> BuscarPorCpfAsync(string cpf);
        Task<ColaboradorEntity?> BuscarPorMatriculaAsync(string numeroMatricula);
        Task<ColaboradorEntity?> AdicionarAsync(ColaboradorEntity colaborador);
        Task<ColaboradorEntity?> EditarAsync(int id, ColaboradorEntity colaborador);
        Task<ColaboradorEntity?> RemoverAsync(int id);
        Task<PageResultModel<IEnumerable<ColaboradorEntity>?>> ListarAsync(int pageNumber = 1, int pageSize = 10);
        Task<PageResultModel<IEnumerable<ColaboradorEntity>?>> ListarPorEmpresaAsync(int empresaId, int pageNumber = 1, int pageSize = 10);
    }
}
