namespace Webshop.Tools.Messaging
{
    public interface IConsumer<T>
    {
        Task Connect(bool durable = false, bool exclusive = false, bool autoDelete = false);
    }
}
