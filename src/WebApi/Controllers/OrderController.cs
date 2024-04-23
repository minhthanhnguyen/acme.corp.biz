using Core.CQRS;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {        
        private readonly ILogger<OrderController> _logger;
        private readonly IAcmeCorpBizService _acmeCorpBizService;

        public OrderController(ILogger<OrderController> logger, IAcmeCorpBizService acmeCorpBizService)
        {
            _logger = logger;
            _acmeCorpBizService = acmeCorpBizService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateOrderRequest createOrderRequest)
        {
            int orderId = await _acmeCorpBizService.CreateOrderAsync(createOrderRequest);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order? order = await _acmeCorpBizService.GetOrderAsync(new GetEntityByIdRequest { Id = id });

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}
