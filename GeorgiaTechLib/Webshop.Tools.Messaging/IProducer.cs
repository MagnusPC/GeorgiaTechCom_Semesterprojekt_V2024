namespace Webshop.Tools.Messaging
{
    public interface IProducer
    {
        Task SendMessage<T>(Message<T> message);
        Task Connect(bool durable = false, bool exclusive = false, bool autoDelete = false);
    }
}
