using System;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TheGramPost.Domain.DTO.Request;
using TheGramPost.Domain.Query.GetPostsOfFollowedUsers;
using TheGramPost.Domain.Query.GetUserPostsPreviewQuery;
using TheGramPost.Properties;

namespace TheGramPost.EventBus
{
    public class EventBusRabbitMqImpl : IEventBusService, IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRabbitMQPersistentConn _persistentConn;
        private readonly IServiceProvider _serviceProvider;
        private IModel _consumerChannel;
        private readonly string _queueName;

        public EventBusRabbitMqImpl(IRabbitMQPersistentConn persistentConn, IServiceProvider serviceProvider,
            string queueName)
        {
            _persistentConn = persistentConn ?? throw new ArgumentNullException(nameof(persistentConn));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _queueName = queueName;
        }

        public IModel CreateConsumerChannel()
        {
            if (!_persistentConn.IsConnected)
            {
                Logger.Error("No connection while creating consumer channels, retrying.");
                _persistentConn.TryConnect();
            }

            var channel = _persistentConn.CreateModel();
            channel.QueueDeclare(_queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(_queueName, autoAck: false, consumer: consumer);
            channel.CallbackException += (sender, ea) =>
            {
                Logger.Error("Channel for queue {0} has crashed", _queueName, ea.Exception);
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };
            Logger.Info("Channel for queue {0} has been created", _queueName);
            _consumerChannel = channel;
            return channel;
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs ea)
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var props = ea.BasicProperties;

            switch (ea.RoutingKey)
            {
                case RabbitMqChannels.GetPostPreviews:
                    SendUserPostPreviewsResponse(message, props, ea.DeliveryTag);
                    break;
                case RabbitMqChannels.GetPostsOfFollowers:
                    var deserializedRequest = JsonConvert.DeserializeObject<PaginatedFeedPostsRequest>(message);
                    SendPostsOfFollowedUsersResponse(deserializedRequest,props,ea.DeliveryTag);
                    break;
            }
        }

        private async void SendUserPostPreviewsResponse(string userId, IBasicProperties props, ulong tag)
        {
            var query = new GetUserPostsPreviewQuery
            {
                UserId = userId
            };
            var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            var result = await mediator.Send(query);
            scope.Dispose();
            var channel = _persistentConn.CreateModel();
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            SendResponse(result, props.ReplyTo, replyProps, tag);
        }

        private async void SendPostsOfFollowedUsersResponse(PaginatedFeedPostsRequest request, IBasicProperties props, ulong tag)
        {
            var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            var result = await mediator.Send(new GetPostsOfFollowedUsersQuery(request.Page,request.FollowedUsers));
            scope.Dispose();
            
            var channel = _persistentConn.CreateModel();
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            SendResponse(result, props.ReplyTo, replyProps, tag);
        }

        private void SendResponse(object body, string queueName, IBasicProperties props, ulong tag)
        {
            var response = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
            _consumerChannel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: props,
                body: response);

            _consumerChannel.BasicAck(deliveryTag: tag,
                multiple: false);
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
        }
    }
}