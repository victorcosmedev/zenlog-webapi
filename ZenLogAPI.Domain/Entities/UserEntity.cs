using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Domain.Entities
{
    [Table("tb_zl_user")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
