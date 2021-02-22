using Content.API.Entities;
using Content.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Content.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IArticleRepository _repository;
        private readonly ILogger<ContentController> _logger;

        public ContentController(IArticleRepository repository, ILogger<ContentController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Article>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles(int limit)
        {
            var articles = await _repository.GetArticles(limit);
            return Ok(articles);
        }

        [Route("{assetId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Article>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Article>> GetArticle(string assetId)
        {
            var article = await _repository.GetArticle(assetId);
            return Ok(article);
        }


        [Route("GetArticlesByCategory/{categoryName}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Article>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesByCategory(string categoryName)
        {
            var articles = await _repository.GetArticlesByCategory(categoryName);
            return Ok(articles);
        }

        [Route("GetCategories")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Article>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var brands = await _repository.GetCategories();
            return Ok(brands);
        }

    }
}
