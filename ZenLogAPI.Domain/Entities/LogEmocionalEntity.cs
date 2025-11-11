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
    [Table("tb_zl_log_emocional")]
    public class LogEmocionalEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public NivelEmocional NivelEmocional { get; set; }
        public string? DescricaoSentimento { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey(nameof(Colaborador))]
        public int ColaboradorId { get; set; }
        [JsonIgnore]
        public ColaboradorEntity Colaborador { get; set; }
    }

    public enum NivelEmocional
    {
        Unknown = 0,
        Excelente = 1,
        Bem = 2,
        Neutro = 3,
        Mal = 4,
        MuitoMal = 5
    }
}
