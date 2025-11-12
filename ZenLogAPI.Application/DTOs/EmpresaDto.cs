using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLogAPI.Domain.Entities;

namespace ZenLogAPI.Application.DTOs
{
    public class EmpresaDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string RazaoSocial { get; set; }
        [Required]
        public SetorEmpresa Setor { get; set; }
    }
}
