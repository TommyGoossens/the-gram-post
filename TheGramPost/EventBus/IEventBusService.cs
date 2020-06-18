using RabbitMQ.Client;

namespace TheGramPost.EventBus
{
    public interface IEventBusService
    {
        public IModel CreateConsumerChannel();
        
        
    }
}