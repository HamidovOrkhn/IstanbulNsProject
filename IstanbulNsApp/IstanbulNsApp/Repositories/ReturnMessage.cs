using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Repositories
{
    public class ReturnMessage<T>
    {
        public int Code { get; set; } 
        public string Message { get; set; }
        public int IsCatched { get; set; } 
        public T Data { get; set; }
        

    }
}
