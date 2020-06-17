using RabbitMQ.Client;

namespace TheGramPost.EventBus
{
    public interface IEventBusImpl
    {
        public IModel CreateConsumerChannel();
    }
}