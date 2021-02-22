using Content.API.Data.Interfaces;
using Content.API.Entities;
using Content.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.API.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IContentContext _context;

        public ArticleRepository(IContentContext contentContext)
        {
            _context = contentContext ?? throw new ArgumentNullException(nameof(contentContext));
        }

        public async Task<IEnumerable<Article>> GetArticles(int? limit = null)
        {
            FilterDefinition<Article> filter = Builders<Article>.Filter.Eq(p => p.IsPublished, true);
            return await _context
                            .Articles
                            .Find(filter)
                            .SortByDescending(t => t.PublicationDateUtc)
                            .Limit(limit)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetLastArticles(int? limit = 5)
        {
            FilterDefinition<Article> filter = Builders<Article>.Filter.Eq(p => p.IsPublished, true);
            return await _context
                            .Articles
                            .Find(filter)
                            .Limit(limit)
                            .SortByDescending(t => t.PublicationDateUtc)
                            .ToListAsync();
        }

        public async Task<Article> GetArticle(string assetId)
        {
            return await _context
                            .Articles
                            .Find(p => p.AssetId == assetId && p.IsPublished == true)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesByCategory(string categoryName)
        {
            return await _context
                          .Articles
                          .Find(p => p.AssetType == categoryName && p.IsPublished == true)
                          .ToListAsync();
        }

        public async Task Create(Article article)
        {
            await _context.Articles.InsertOneAsync(article);
        }

        public async Task<bool> Update(Article article)
        {
            var updateResult = await _context
                                        .Articles
                                        .ReplaceOneAsync(filter: g => g.Id == article.Id, replacement: article);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Article> filter = Builders<Article>.Filter.Eq(m => m.AssetId, id);
            DeleteResult deleteResult = await _context
                                                .Articles
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            var filter = Builders<Article>.Filter.Ne(x => x.AssetType, null);
            var categories = await _context.Articles.DistinctAsync(x => x.AssetType, filter);
            return await categories.ToListAsync();
        }
    }
}
