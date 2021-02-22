using Admin.API.Entities;
using AutoMapper;
using EventBusRabbitMQ.Events;

namespace Admin.API.Mapping
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<ArticlePublished, ArticlePublishedEvent>().ReverseMap();
            CreateMap<ArticleWithdrawn, ArticleWithdrawnEvent>().ReverseMap();
        }
    }
}
