using Content.API.Data.Interfaces;
using Content.API.Entities;
using Content.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Content.API.Repositories
{
    public class ArticleAuditRepository : IArticleAuditRepository
    {
        private readonly IContentContext _context;

        public ArticleAuditRepository(IContentContext contentContext)
        {
            _context = contentContext ?? throw new ArgumentNullException(nameof(contentContext));
        }

        public async Task<IEnumerable<ArticleAudit>> GetArticleAudits(int? limit = null)
        {
            FilterDefinition<ArticleAudit> filter = Builders<ArticleAudit>.Filter.Eq(p => p.IsPublished, true);
            return await _context
                            .ArticleAudits
                            .Find(filter)
                            .Limit(limit)
                            .ToListAsync();
        }

        public async Task<IEnumerable<ArticleAudit>> GetLastArticleAudits(int? limit = 5)
        {
            var withdrawns = await _context
                                    .ArticleAudits
                                    .Find(t => t.IsPublished == false)
                                    .Project(t => t.AssetId)
                                    .ToListAsync();
            FilterDefinition<ArticleAudit> filter = Builders<ArticleAudit>.Filter.Eq(p => p.IsPublished, true);
            return await _context
                            .ArticleAudits
                            .Find(filter)
                            .Limit(limit)
                            .SortByDescending(t => t.PublicationDateUtc)
                            .ToListAsync();
        }

        public async Task<ArticleAudit> GetArticleAudit(string assetId, int version)
        {
            return await _context
                            .ArticleAudits
                            .Find(p => p.AssetId == assetId && p.Version == version && p.IsPublished == true)
                            //.SortByDescending(e => e.Version)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ArticleAudit>> GetArticleAuditsByCategory(string categoryName)
        {
            return await _context
                          .ArticleAudits
                          .Find(p => p.AssetType == categoryName && p.IsPublished == true)
                          .ToListAsync();
        }

        public async Task Create(ArticleAudit articleAudit)
        {
            await _context.ArticleAudits.InsertOneAsync(articleAudit);
        }

        public async Task<bool> Update(ArticleAudit articleAudit)
        {
            var updateResult = await _context
                                        .ArticleAudits
                                        .ReplaceOneAsync(filter: g => g.Id == articleAudit.Id, replacement: articleAudit);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<ArticleAudit> filter = Builders<ArticleAudit>.Filter.Eq(m => m.AssetId, id);
            DeleteResult deleteResult = await _context
                                                .ArticleAudits
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
