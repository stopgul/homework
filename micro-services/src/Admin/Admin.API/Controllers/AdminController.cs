using Admin.API.Entities;
using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Admin.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<AdminController> _logger;
        private readonly IMapper _mapper;

        public AdminController(EventBusRabbitMQProducer eventBus, ILogger<AdminController> logger, IMapper mapper)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("publish")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult PublishArticlePublished([FromBody] ArticlePublished articlePublished)
        {
            var eventMessage = _mapper.Map<ArticlePublishedEvent>(articlePublished);
            eventMessage.RequestId = Guid.NewGuid();
            try
            {
                _eventBus.PublishArticlePublished(EventBusConstants.ArticlePublishedQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR Publishing integration event: {eventMessage.RequestId} from ArticlePublished");
                throw;
            }
            return Accepted();
        }

        [Route("withdraw")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult WithdrawArticlePublished([FromBody] ArticleWithdrawn articleWithdrawn)
        {
            var eventMessage = _mapper.Map<ArticleWithdrawnEvent>(articleWithdrawn);
            eventMessage.RequestId = Guid.NewGuid();
            try
            {
                _eventBus.PublishArticleWithdrawn(EventBusConstants.ArticlePublishedQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR Publishing integration event: {eventMessage.RequestId} from ArticleWithdrawn");
                throw;
            }
            return Accepted();
        }
    }
}
