using Content.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Content.API.Data
{
    public class ContentContextSeed
    {
        public static void SeedData(IMongoCollection<Article> contentCollection
            , IMongoCollection<ArticleAudit> contentAuditCollection)
        {
            bool existArticle = contentCollection.Find(p => true).Any();
            if (!existArticle)
            {
                contentCollection.InsertManyAsync(GetPreconfiguredArticles());
            }
            bool existArticleAudit = contentAuditCollection.Find(p => true).Any();
            if (!existArticleAudit)
            {
                contentAuditCollection.InsertManyAsync(GetPreconfiguredArticleAudits());
            }
        }

        private static IEnumerable<ArticleAudit> GetPreconfiguredArticleAudits()
        {
            return new List<ArticleAudit>()
            {
                new ArticleAudit()
                {
                    AssetId= "0-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "retail-clinics-next-frontier-of-primary-care-delivery-patients-receptive",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author1",
                    AssetType = "Article-Science",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new ArticleAudit()
                {
                    AssetId= "1-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "micro-services",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author2",
                    AssetType = "Article-Tech",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new ArticleAudit()
                {
                    AssetId= "2-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "no-sql-db",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author2",
                    AssetType = "Article-Tech",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new ArticleAudit()
                {
                    AssetId= "3-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "aws-cloudwatch",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,14,3,30,0),
                    Author = "author2",
                    AssetType = "Article-Tech",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new ArticleAudit()
                {
                    AssetId= "4-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "what's-next-for-the-mars-perseverance-rover?",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author1",
                    AssetType = "Article-Science",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
            };
        }

        private static IEnumerable<Article> GetPreconfiguredArticles()
        {
            return new List<Article>()
            {
                new Article()
                {
                    AssetId= "0-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "retail-clinics-next-frontier-of-primary-care-delivery-patients-receptive",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author= "author2",
                    AssetType="Article-Science",
                    Body="html-of-article",
                    Geographies=new List<string>(){"United States","UK" },
                    Topics=new List<string>(){"Digital Health", "Security" },
                    Version=1,
                    IsPublished=true
                },
                new Article()
                {
                    AssetId= "1-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "micro-services",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author1",
                    AssetType = "Article-Tech",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new Article()
                {
                    AssetId= "2-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "no-sql-db",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author3",
                    AssetType = "Article-Tech",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new Article()
                {
                    AssetId= "3-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "aws-cloudwatch",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,14,3,30,0),
                    Author = "author3",
                    AssetType = "Article-Tech",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
                new Article()
                {
                    AssetId= "4-f9b15f5-f048-d9f8-b9f1-e201f110b22f" ,
                    Slug= "what's-next-for-the-mars-perseverance-rover?",
                    Thumbnail= "https://dummyimage.com/150",
                    Url= "https://dummyimage.com/600",
                    PublicationDateUtc= new DateTime(2020,3,13,3,30,0),
                    Author = "author1",
                    AssetType = "Article-Science",
                    Body = "html-of-article",
                    Geographies = new List<string>() { "United States", "UK" },
                    Topics = new List<string>() { "Digital Health", "Security" },
                    Version = 0,
                    IsPublished = true
                },
            };
        }
    }
}
