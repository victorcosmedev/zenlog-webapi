using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Application.DTOs.Empresa;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Application.Mappers
{
    public static class EmpresaMapper
    {
        public static EmpresaEntity ToEntity(this EmpresaDto dto)
        {
            return new EmpresaEntity
            {
                Id = dto.Id,
                RazaoSocial = dto.RazaoSocial,
                Setor = dto.Setor
            };
        }

        public static EmpresaDto ToDto(this EmpresaEntity entity)
        {
            return new EmpresaDto
            {
                Id = entity.Id,
                RazaoSocial = entity.RazaoSocial,
                Setor = entity.Setor
            };
        }
    }
}
