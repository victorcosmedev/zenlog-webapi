using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Application.DTOs.LogEmocional
{
    public class LogEmocionalPredicaoDto
    {
        public float FezExercicios { get; set; }
        public float HorasDescanso { get; set; }
        public float LitrosAgua { get; set; }
        public float NivelEmocional { get; set; }
    }
}