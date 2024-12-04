using Webshop.Tools.DataWrappers;

namespace DataWrappers
{
    public class CatWrapper<T>
    {
        public T Data { get; set; }
        public OperationType Type { get; set; }

        public CatWrapper()
        {

        }

        public CatWrapper(T data, CatWrapper<T>.OperationType type)
        {
            Data = data;
            Type = type;
        }

        public enum OperationType
        {
            Loaf,
            Orange,
            Vibin,
            CatShapedVoid,
            Cool,
            Undefined
        }
    }
}
