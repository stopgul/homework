using Content.API.Data.Interfaces;
using Content.API.Entities;
using Content.API.Settings;
using MongoDB.Driver;

namespace Content.API.Data
{
    public class ContentContext : IContentContext
    {
        public ContentContext(IContentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Articles = database.GetCollection<Article>(settings.CollectionName);
            ArticleAudits = database.GetCollection<ArticleAudit>(settings.CollectionAuditName);
            ContentContextSeed.SeedData(Articles, ArticleAudits);
        }

        public IMongoCollection<Article> Articles { get; }
        public IMongoCollection<ArticleAudit> ArticleAudits { get; }
    }
}
