using Content.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.API.Repositories.Interfaces
{
    public interface IArticleAuditRepository
    {
        Task Create(ArticleAudit article);
        Task<bool> Delete(string id);
        Task<ArticleAudit> GetArticleAudit(string assetId, int version);
        Task<IEnumerable<ArticleAudit>> GetArticleAudits(int? limit = null);
        Task<IEnumerable<ArticleAudit>> GetArticleAuditsByCategory(string categoryName);
        Task<IEnumerable<ArticleAudit>> GetLastArticleAudits(int? limit = 5);
        Task<bool> Update(ArticleAudit article);
    }
}