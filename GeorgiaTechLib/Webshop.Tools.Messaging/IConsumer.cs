namespace Webshop.Tools.Messaging
{
    public interface IConsumer
    {
        Task Connect(bool durable = false, bool exclusive = false, bool autoDelete = false);
    }
}
