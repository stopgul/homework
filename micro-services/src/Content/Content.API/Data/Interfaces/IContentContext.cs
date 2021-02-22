using Content.API.Entities;
using MongoDB.Driver;

namespace Content.API.Data.Interfaces
{
    public interface IContentContext
    {
        IMongoCollection<Article> Articles { get; }
        IMongoCollection<ArticleAudit> ArticleAudits { get; }
    }
}
