using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Application.DTOs
{
    public class ColaboradorDto
    {
        [Required]
        [Range(0, int.MaxValue)]
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
        [Required]
        [Range(0, int.MaxValue)]
        public int EmpresaId { get; set; }
    }
}
