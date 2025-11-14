using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Domain.Entities
{
    [Table("tb_zlg_empresa")]
    public class EmpresaEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string RazaoSocial { get; set; }
        [Required]
        public SetorEmpresa Setor { get; set; }
        public List<ColaboradorEntity>? Colaboradores { get; set; }
    }

    public enum SetorEmpresa
    {
        Unknown = 0,
        Tecnologia = 1,
        Varejo = 2,
        Saude = 3,
        Educacao = 4,
        Financeiro = 5,
        Industria = 6,
        Agricultura = 7,
        Transporte = 8,
        Turismo = 9,
        Outros = 10
    }
}
