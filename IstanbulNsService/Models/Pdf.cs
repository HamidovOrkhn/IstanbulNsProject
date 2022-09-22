using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class Pdf
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int OnlineQueryId { get; set; }
        public OnlineQuery OnlineQuery { get; set; }
    }
}
