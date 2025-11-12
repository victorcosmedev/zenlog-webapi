using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Application.Mappers
{
    public static class ColaboradorMapper
    {
        public static ColaboradorEntity ToEntity(this ColaboradorDto dto)
        {
            return new ColaboradorEntity
            {
                Id = dto.Id,
                Username = dto.Username,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento,
                NumeroMatricula = dto.NumeroMatricula,
                Cpf = dto.Cpf,
                EmpresaId = dto.EmpresaId
            };
        }

        public static ColaboradorDto ToDto(this ColaboradorEntity entity)
        {
            return new ColaboradorDto
            {
                Id = entity.Id,
                Username = entity.Username,
                Email = entity.Email,
                DataNascimento = entity.DataNascimento,
                NumeroMatricula = entity.NumeroMatricula,
                Cpf = entity.Cpf,
                EmpresaId = entity.EmpresaId
            };
        }
    }
}
