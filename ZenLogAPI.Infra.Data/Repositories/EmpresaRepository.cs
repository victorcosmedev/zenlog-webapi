using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;
using ZenLogAPI.Domain.Interfaces;
using ZenLogAPI.Domain.Models.PageResultModel;
using ZenLogAPI.Infra.Data.AppData;

namespace ZenLogAPI.Infra.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ApplicationContext _context;

        public EmpresaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<EmpresaEntity?> AdicionarAsync(EmpresaEntity empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<EmpresaEntity?> EditarAsync(int id, EmpresaEntity empresa)
        {
            var empresaExistente = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);

            if (empresaExistente == null) return null;

            empresaExistente.RazaoSocial = empresa.RazaoSocial;
            empresaExistente.Setor = empresa.Setor;

            await _context.SaveChangesAsync();
            return empresaExistente;
        }

        public async Task<EmpresaEntity?> RemoverAsync(int id)
        {
            var empresaExistente = await _context.Empresas
                .Include(e => e.Colaboradores)
                .ThenInclude(c => c.LogsEmocionais)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (empresaExistente == null) return null;

            if (empresaExistente.Colaboradores != null && empresaExistente.Colaboradores.Count > 0)
            {
                foreach (var colaborador in empresaExistente.Colaboradores)
                {
                    if (colaborador.LogsEmocionais != null && colaborador.LogsEmocionais.Count > 0)
                    {
                        _context.LogsEmocionais.RemoveRange(colaborador.LogsEmocionais);
                    }
                }
                _context.Colaboradores.RemoveRange(empresaExistente.Colaboradores);
            }

            _context.Empresas.Remove(empresaExistente);
            await _context.SaveChangesAsync();
            return empresaExistente;
        }

        public async Task<EmpresaEntity?> BuscarPorIdAsync(int id)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PageResultModel<IEnumerable<EmpresaEntity>?>> ListarAsync(int pageNumber = 1, int pageSize = 10)
        {
            var totalItems = await _context.Empresas.CountAsync();

            var empresas = await _context.Empresas
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var pageResultModel = new PageResultModel<IEnumerable<EmpresaEntity>>
            {
                Items = empresas,
                NumeroPagina = pageNumber,
                TamanhoPagina = pageSize,
                TotalItens = totalItems
            };

            return pageResultModel;
        }
    }
}
