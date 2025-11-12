using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Application.DTOs
{
    public class LogEmocionalDto
    {
        public int Id { get; set; }
        public NivelEmocional NivelEmocional { get; set; }
        public string? DescricaoSentimento { get; set; }
        public bool FezExercicios { get; set; }
        public int HorasDescanso { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ColaboradorId { get; set; }
    }
}
