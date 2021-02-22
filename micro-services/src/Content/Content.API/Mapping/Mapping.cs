using AutoMapper;
using Content.API.Entities;
using EventBusRabbitMQ.Events;

namespace Content.API.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ArticlePublishedEvent, Article>().ReverseMap();
            CreateMap<ArticlePublishedEvent, ArticleAudit>().ReverseMap();
        }
    }
}
