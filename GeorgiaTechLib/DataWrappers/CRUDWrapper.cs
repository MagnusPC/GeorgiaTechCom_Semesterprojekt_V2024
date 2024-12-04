
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Tools.DataWrappers
{
    public class CRUDWrapper<T>
    {
        public T Data { get; set; }
        public OperationType Type { get; set; }

        public CRUDWrapper() 
        { 
            
        }

        public CRUDWrapper(T data, CRUDWrapper<T>.OperationType type)
        {
            Data = data;
            Type = type;
        }

        public enum OperationType
        {
            Create,
            Read,
            Update,
            Delete,
            
            Undefined
        }
    }

    

}
