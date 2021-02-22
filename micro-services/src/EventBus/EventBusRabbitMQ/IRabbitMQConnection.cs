using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQConnection
    {
        bool IsConnected { get; }

        IModel CreateModel();
        void Dispose();
        bool TryConnect();
    }
}