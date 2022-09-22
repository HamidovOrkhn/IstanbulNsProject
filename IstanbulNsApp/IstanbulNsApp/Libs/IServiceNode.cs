using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstanbulNsApp.Repositories;

namespace IstanbulNsApp.Libs
{
    public interface IServiceNode<T,U>
    {
        public ReturnMessage<U> DeleteClient(string url, string token = null);
        public ReturnMessage<U> GetClient(string url, string token = null);
        public ReturnMessage<U> PostClient(object data, string url, string token = null);
        public ReturnMessage<U> PutClient(object data, string url, string token = null);

    }
}
