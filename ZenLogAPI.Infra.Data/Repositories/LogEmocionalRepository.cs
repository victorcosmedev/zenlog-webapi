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
    public class LogEmocionalRepository : ILogEmocionalRepository
    {
        private readonly ApplicationContext _context;

        public LogEmocionalRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<LogEmocionalEntity?> AdicionarAsync(LogEmocionalEntity log)
        {
            var colaboradorExiste = await _context.Colaboradores.AnyAsync(c => c.Id == log.ColaboradorId);

            if (colaboradorExiste == false) return null;

            await _context.LogsEmocionais.AddAsync(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<LogEmocionalEntity?> EditarAsync(int id, LogEmocionalEntity log)
        {
            var logExistente = await _context.LogsEmocionais.FirstOrDefaultAsync(l => l.Id == id);

            if (logExistente == null) return null;

            var colaboradorExiste = await _context.Colaboradores.AnyAsync(c => c.Id == log.ColaboradorId);

            if (colaboradorExiste == false) return null;

            logExistente.NivelEmocional = log.NivelEmocional;
            logExistente.DescricaoSentimento = log.DescricaoSentimento;

            await _context.SaveChangesAsync();
            return logExistente;
        }

        public async Task<LogEmocionalEntity?> RemoverAsync(int id)
        {
            var logExistente = await _context.LogsEmocionais.FirstOrDefaultAsync(l => l.Id == id);

            if (logExistente == null) return null;

            _context.LogsEmocionais.Remove(logExistente);
            await _context.SaveChangesAsync();
            return logExistente;
        }

        public async Task<LogEmocionalEntity?> BuscarPorIdAsync(int id)
        {
            return await _context.LogsEmocionais
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<PageResultModel<IEnumerable<LogEmocionalEntity>>> ListarPorColaboradorAsync(int colaboradorId, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.LogsEmocionais
                .Where(l => l.ColaboradorId == colaboradorId);

            var totalItems = await query.CountAsync();

            var logs = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip(pageNumber - 1 * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pageResultModel = new PageResultModel<IEnumerable<LogEmocionalEntity>>
            {
                Items = logs,
                NumeroPagina = pageNumber,
                TamanhoPagina = pageSize,
                TotalItens = totalItems
            };

            return pageResultModel;
        }
    }
}
