namespace Webshop.SearchDBAccess.API
{
    using Webshop.Tools.DataWrappers;
    using Webshop.Tools.Messaging;

    public class DBAccess
    {
        public void UpdateDatabase(Message<CRUDWrapper<List<Book>>> messageObj)
        {
            switch (messageObj.Content.Type)
            {
                case CRUDWrapper<List<Book>>.OperationType.Create:
                    Add(messageObj.Content.Data);
                    break;

                case CRUDWrapper<List<Book>>.OperationType.Update:
                    Update(messageObj.Content.Data);
                    break;

                case CRUDWrapper<List<Book>>.OperationType.Delete:
                    Delete(messageObj.Content.Data);
                    break;

                case CRUDWrapper<List<Book>>.OperationType.Undefined:
                    break;
                default:
                    break;
            }
        }
        void Add(List<Book> books)
        {

        }
        void Update(List<Book> books)
        {

        }
        void Delete(List<Book> books)
        {

        }
    }





    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }






}
