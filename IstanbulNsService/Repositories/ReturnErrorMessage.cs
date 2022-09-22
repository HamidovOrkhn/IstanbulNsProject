using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Repositories
{
    public class ReturnErrorMessage
    {
        
        public int Code { get; set; } = 400;
        public string Message { get; set; } = "Some misunderstanding occured";
        public int IsCatched { get; set; } = 1;
        public int ErrorType { get; set; }
        public object Data { get; set; }
        public ReturnErrorMessage(int errortype, object data = null, int code = 400, int isCatched = 1, string message = "Some misunderstanding occured")
        {
            this.Code = code;
            this.Message = message;
            this.IsCatched = isCatched;
            this.ErrorType = errortype;
            this.Data = data;
        }
    }
}
