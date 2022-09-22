using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class InfoStr:BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Info { get; set; }
    }
}
