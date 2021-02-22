using System;

namespace EventBusRabbitMQ.Events
{
    public class ArticleWithdrawnEvent
    {
        public Guid RequestId { get; set; }
        public string AssetId { get; set; }
        public int Version { get; set; }
    }
}
