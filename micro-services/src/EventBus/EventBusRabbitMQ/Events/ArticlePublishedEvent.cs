using System;
using System.Collections.Generic;

namespace EventBusRabbitMQ.Events
{
    public class ArticlePublishedEvent
    {
        public Guid RequestId { get; set; }
        public string AssetId { get; set; }
        public string Slug { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string PublicationDateUtc { get; set; }//DateTime
        public string Author { get; set; }
        public string AssetType { get; set; }
        public string Body { get; set; }
        public List<string> Geographies { get; set; }
        public List<string> Topics { get; set; }
        public int Version { get; set; }
    }
}
