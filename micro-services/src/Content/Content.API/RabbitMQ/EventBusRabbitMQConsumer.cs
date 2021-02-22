using AutoMapper;
using Content.API.Entities;
using Content.API.Repositories.Interfaces;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Content.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMapper _mapper;
        private readonly IArticleRepository _repoArticle;
        private readonly IArticleAuditRepository _repoArticleAudit;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection,
            IMapper mapper,
            IArticleRepository repoArticle,
            IArticleAuditRepository repoArticleAudit)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repoArticle = repoArticle ?? throw new ArgumentNullException(nameof(repoArticle));
            _repoArticleAudit = repoArticleAudit ?? throw new ArgumentNullException(nameof(repoArticleAudit));
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.ArticlePublishedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            //Create event when something receive
            consumer.Received += ReceivedEvent;
            string tag = channel.BasicConsume(queue: EventBusConstants.ArticlePublishedQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.ArticlePublishedQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                if (message.ToLower().Contains("slug") && message.ToLower().Contains("publicationdateutc"))
                {
                    var @event = JsonConvert.DeserializeObject<ArticlePublishedEvent>(message);
                    var article = _mapper.Map<Article>(@event);
                    article.IsPublished = true;
                    var articleAudit = _mapper.Map<ArticleAudit>(@event);
                    articleAudit.IsPublished = true;
                    await _repoArticleAudit.Create(articleAudit);
                    var articleExist = _repoArticle.GetArticle(article.AssetId);
                    if (articleExist.Result != null)
                    {
                        await _repoArticle.Delete(article.AssetId);
                    }
                    await _repoArticle.Create(article);
                }
                else
                {
                    var @event = JsonConvert.DeserializeObject<ArticleWithdrawnEvent>(message);
                    var article = await _repoArticle.GetArticle(@event.AssetId);
                    article.Version = @event.Version;
                    article.IsPublished = false;
                    await _repoArticle.Update(article);
                    var articleAudit = await _repoArticleAudit.GetArticleAudit(@event.AssetId, @event.Version - 1);
                    articleAudit.Version = @event.Version;
                    articleAudit.IsPublished = false;
                    await _repoArticleAudit.Update(articleAudit);
                }
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }

    }
}
