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
using TheGramPost.EventBus.Connection;
using TheGramPost.Properties;

namespace TheGramPost.EventBus.Channels
{
    public class RabbitMQQueueChannel : RabbitMQAbstractChannel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly RabbitMQBaseConnection _connection;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _queueName;
        public RabbitMQQueueChannel(RabbitMQBaseConnection connection, IServiceProvider serviceProvider,
            string queueName)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
            _queueName = queueName;
        }
        
        public override IModel DeclareChannel()
        {
            if (!_connection.IsConnected)
            {
                Logger.Error("No connection while creating consumer channels, retrying.");
                _connection.TryConnect();
            }

            ConsumerChannel = _connection.CreateModel();
            ConsumerChannel.QueueDeclare(_queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(ConsumerChannel);

            consumer.Received += ReceivedEvent;
            ConsumerChannel.CallbackException += CallbackException;
            ConsumerChannel.BasicConsume(_queueName, autoAck: false, consumer: consumer);
            
            return ConsumerChannel;
        }
        
        private void ReceivedEvent(object sender, BasicDeliverEventArgs ea)
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
                default:
                    Logger.Info(ea.RoutingKey);
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
            var channel = _connection.CreateModel();
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
            
            var channel = _connection.CreateModel();
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            SendResponse(result, props.ReplyTo, replyProps, tag);
        }


        
        private void SendResponse(object body,string queueName, IBasicProperties props, ulong tag)
        {
            var response = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
            ConsumerChannel.BasicPublish(
                exchange: "",
                routingKey: queueName, 
                basicProperties: props,
                body: response);
            
            ConsumerChannel.BasicAck(deliveryTag: tag,
                multiple: false);
        }


        public override void Dispose()
        {
            ConsumerChannel?.Dispose();
        }
    }
}