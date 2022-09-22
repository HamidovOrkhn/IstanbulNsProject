using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Models.MessageBird
{
    public class MessageBirdResponse
    {
        public string Id { get; set; }
        public string Href { get; set; }
        public string Direction { get; set; }
        public string Type { get; set; }
        public string Body { get; set; }
        public string Originator { get; set; }
        public int GateWay { get; set; }
    }
}
