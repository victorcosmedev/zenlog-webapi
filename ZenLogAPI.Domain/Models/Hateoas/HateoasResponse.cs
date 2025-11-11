using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLogAPI.Domain.Models.Hateoas
{
    public class HateoasResponse<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
