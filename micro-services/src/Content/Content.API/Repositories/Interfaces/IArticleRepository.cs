using Content.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.API.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        Task Create(Article article);
        Task<bool> Delete(string id);
        Task<Article> GetArticle(string id);
        Task<IEnumerable<Article>> GetArticles(int? limit = null);
        Task<IEnumerable<Article>> GetArticlesByCategory(string categoryName);
        Task<bool> Update(Article article);
        Task<IEnumerable<string>> GetCategories();
    }
}