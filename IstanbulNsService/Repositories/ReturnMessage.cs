using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Repositories
{
    public class ReturnMessage
    {
        public int Code { get; set; } = 200;
        public string Message { get; set; }
        public int IsCatched { get; set; } = 0;
        public object Data { get; set; }
        public ReturnMessage(object data = null, int code = 200, int isCatched = 0, string message = "Success")
        {
            this.Code = code;
            this.Message = message;
            this.IsCatched = isCatched;
            this.Message = message;
            this.Data = data;
        }

    }
}
