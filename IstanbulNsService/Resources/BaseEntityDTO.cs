using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class BaseEntityDTO
    {
        public int Id { get; set; }
        [Required]
        public int LangId { get; set; }
    }
}
