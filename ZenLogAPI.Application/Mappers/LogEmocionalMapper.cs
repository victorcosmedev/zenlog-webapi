using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Application.Mappers
{
    public class LogEmocionalMapper
    {
        public static LogEmocionalEntity ToEntity(LogEmocionalDto dto)
        {
            return new LogEmocionalEntity
            {
                Id = dto.Id,
                NivelEmocional = dto.NivelEmocional,
                DescricaoSentimento = dto.DescricaoSentimento,
                FezExercicios = dto.FezExercicios,
                HorasDescanso = dto.HorasDescanso,
                CreatedAt = dto.CreatedAt,
                ColaboradorId = dto.ColaboradorId
            };
        }

        public static LogEmocionalDto ToDto(LogEmocionalEntity entity)
        {
            return new LogEmocionalDto
            {
                Id = entity.Id,
                NivelEmocional = entity.NivelEmocional,
                DescricaoSentimento = entity.DescricaoSentimento,
                FezExercicios = entity.FezExercicios,
                HorasDescanso = entity.HorasDescanso,
                CreatedAt = entity.CreatedAt,
                ColaboradorId = entity.ColaboradorId
            };
        }
    }
}
