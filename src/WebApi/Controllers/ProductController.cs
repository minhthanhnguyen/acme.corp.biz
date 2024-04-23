using Core.CQRS;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {        
        private readonly ILogger<ProductController> _logger;
        private readonly IAcmeCorpBizService _acmeCorpBizService;

        public ProductController(ILogger<ProductController> logger, IAcmeCorpBizService acmeCorpBizService)
        {
            _logger = logger;
            _acmeCorpBizService = acmeCorpBizService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateProductRequest createProductRequest)
        {
            int productId = await _acmeCorpBizService.CreateProductAsync(createProductRequest);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            Product? product = await _acmeCorpBizService.GetProductAsync(new GetEntityByIdRequest { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
