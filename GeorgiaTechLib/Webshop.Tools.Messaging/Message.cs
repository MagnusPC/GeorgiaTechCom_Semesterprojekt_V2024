using System.Text.Json.Serialization;

namespace Webshop.Tools.Messaging
{
    public class Message<T>
    {
        public Message()
        {

        }

        public Message(string type, T content)
        {
            MessageType = type;
            Content = content;
        }

        public string MessageType { get; set; }
        public T Content { get; set; }
}
}
