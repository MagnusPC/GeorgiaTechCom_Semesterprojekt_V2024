namespace Webshop.Tools.Messaging
{
    public class Message<T>
    {
        public Message()
        {

        }

        public Message(string type, T content, string correlationId = null)
        {
            MessageType = type;
            Content = content;
            CorrelationId = correlationId;
        }

        public string MessageType { get; set; }
        public T Content { get; set; }
        public string? CorrelationId { get; set; }
    }
}
