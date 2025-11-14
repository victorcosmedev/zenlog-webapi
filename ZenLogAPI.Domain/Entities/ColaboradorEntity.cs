using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZenLogAPI.Domain.Entities
{
    [Table("tb_zlg_colaborador")]
    [Index(nameof(Email), Name = "IDX_colaborador_email")]
    [Index(nameof(NumeroMatricula), Name = "IDX_colaborador_matricula")]
    [Index(nameof(Cpf), Name = "IDX_colaborador_cpf")]
    public class ColaboradorEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        [StringLength(10)]
        [Range(10, 10)]
        public string NumeroMatricula { get; set; }
        [Required]
        [StringLength(11)]
        [Range(11, 11)]
        public string Cpf { get; set; }
        [ForeignKey(nameof(Empresa))]
        public int EmpresaId { get; set; }
        [JsonIgnore]
        public EmpresaEntity Empresa { get; set; }
        public List<LogEmocionalEntity>? LogsEmocionais { get; set; }
    }
}
