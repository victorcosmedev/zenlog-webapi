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
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly ApplicationContext _context;
        public ColaboradorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ColaboradorEntity?> AdicionarAsync(ColaboradorEntity colaborador)
        {
            var empresaExiste = await _context.Empresas.AnyAsync(e => e.Id == colaborador.EmpresaId);

            if (empresaExiste == false) return null;

            await _context.Colaboradores.AddAsync(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<ColaboradorEntity?> BuscarPorCpfAsync(string cpf)
        {
            return await _context.Colaboradores
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<ColaboradorEntity?> BuscarPorEmailAsync(string email)
        {
            return await _context.Colaboradores
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<ColaboradorEntity?> BuscarPorIdAsync(int id)
        {
            return await _context.Colaboradores
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ColaboradorEntity?> BuscarPorMatriculaAsync(string numeroMatricula)
        {
            return await _context.Colaboradores
                .FirstOrDefaultAsync(c => c.NumeroMatricula == numeroMatricula);
        }

        public async Task<ColaboradorEntity?> EditarAsync(int id, ColaboradorEntity colaborador)
        {
            var colaboradorExistente = await _context.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);

            if (colaboradorExistente == null) return null;

            var empresaExiste = await _context.Empresas.AnyAsync(e => e.Id == colaborador.EmpresaId);

            if(empresaExiste == false) return null;


            colaboradorExistente.Username = colaborador.Username;
            colaboradorExistente.Email = colaborador.Email;
            colaboradorExistente.DataNascimento = colaborador.DataNascimento;
            colaboradorExistente.NumeroMatricula = colaborador.NumeroMatricula;
            colaboradorExistente.Cpf = colaborador.Cpf;
            colaboradorExistente.EmpresaId = colaborador.EmpresaId;

            await _context.SaveChangesAsync();
            return colaboradorExistente;
        }

        public async Task<PageResultModel<IEnumerable<ColaboradorEntity>?>> ListarAsync(int pageNumber = 1, int pageSize = 10)
        {
            var colaboradores = await _context.Colaboradores
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageResultModel = new PageResultModel<IEnumerable<ColaboradorEntity>>
            {
                Items = colaboradores,
                NumeroPagina = pageNumber,
                TamanhoPagina = pageSize,
                TotalItens = await _context.Colaboradores.CountAsync()
            };

            return pageResultModel;
        }

        public async Task<PageResultModel<IEnumerable<ColaboradorEntity>?>> ListarPorEmpresaAsync(int empresaId, int pageNumber = 1, int pageSize = 10)
        {
            var colaboradores = await _context.Colaboradores
                .Where(x => x.EmpresaId == empresaId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItens = await _context.Colaboradores.CountAsync();

            var pageResultModel = new PageResultModel<IEnumerable<ColaboradorEntity>?>
            {
                Items = colaboradores,
                NumeroPagina = pageNumber,
                TamanhoPagina = pageSize,
                TotalItens = totalItens
            };

            return pageResultModel;
        }

        public async Task<ColaboradorEntity?> RemoverAsync(int id)
        {
            var colaboradorExistente = await _context.Colaboradores
                .Include(x => x.LogsEmocionais)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (colaboradorExistente == null) return null;

            if(colaboradorExistente.LogsEmocionais != null && colaboradorExistente.LogsEmocionais.Count > 0)
            {
                _context.LogsEmocionais.RemoveRange(colaboradorExistente.LogsEmocionais);
            }

            _context.Colaboradores.Remove(colaboradorExistente);
            await _context.SaveChangesAsync();
            return colaboradorExistente;
        }
    }
}
