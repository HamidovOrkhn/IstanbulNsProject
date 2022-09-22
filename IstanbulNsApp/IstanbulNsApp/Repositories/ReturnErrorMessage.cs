using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Repositories
{
    public class ReturnErrorMessage
    {
        
        public int Code { get; set; } 
        public string Message { get; set; }
        public int IsCatched { get; set; }
        public int ErrorType { get; set; }
        public object Data { get; set; }
        
    }
}
