using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Content.API.Entities
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AssetId { get; set; }
        public string Slug { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public DateTime PublicationDateUtc { get; set; }//DateTime
        public string Author { get; set; }
        public string AssetType { get; set; }
        public string Body { get; set; }
        public List<string> Geographies { get; set; }
        public List<string> Topics { get; set; }
        public int Version { get; set; }
        public bool IsPublished { get; set; }
    }

    public class ArticleAudit : Article
    {
    }
}
