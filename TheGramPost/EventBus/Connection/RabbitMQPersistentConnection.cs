using System;
using System.Collections.Generic;
using NLog;
using TheGramPost.EventBus.Channels;
using TheGramPost.Properties;

namespace TheGramPost.EventBus.Connection
{
    public class RabbitMqPersistentConnection : RabbitMQBaseConnection
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly List<RabbitMQAbstractChannel> _channels = new List<RabbitMQAbstractChannel>();

        //Used to create a Mediator service in a singleton
        protected readonly IServiceProvider ServiceProvider;

        public RabbitMqPersistentConnection(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void CreatePersistentChannels()
        {
            if (!IsConnected)
            {
                Logger.Error("No connection while creating consumer channels, retrying.");
                TryConnect();
            }

            var rpcGetPostPreviewsChannel = new RabbitMQQueueChannel(this,ServiceProvider, RabbitMqChannels.GetPostPreviews);
            rpcGetPostPreviewsChannel.DeclareChannel();
            _channels.Add(rpcGetPostPreviewsChannel);

            var rpcGetPostsOfFollowersChannel = new RabbitMQQueueChannel(this,ServiceProvider, RabbitMqChannels.GetPostsOfFollowers);
            rpcGetPostsOfFollowersChannel.DeclareChannel();
            _channels.Add(rpcGetPostsOfFollowersChannel);
        }

        public void DisconnectChannels()
        {
            foreach (var channel in _channels)
            {
                channel.Dispose();
            }
            Disconnect();
        }
    }
}