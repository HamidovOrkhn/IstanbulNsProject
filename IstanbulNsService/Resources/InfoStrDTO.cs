using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class InfoStrDTO:BaseEntityDTO
    {
        public int DoctorId { get; set; }
        public string Info { get; set; }
    }
}
