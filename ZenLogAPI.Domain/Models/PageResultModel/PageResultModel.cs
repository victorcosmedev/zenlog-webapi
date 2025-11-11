using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Domain.Models.PageResultModel
{
    public class PageResultModel<T>
    {
        public required T Items { get; set; }
        public int TotalItens { get; set; }
        public int NumeroPagina { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
