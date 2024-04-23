using Core.CQRS;
using Core.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {        
        private readonly ILogger<CustomerController> _logger;
        private readonly IAcmeCorpBizService _acmeCorpBizService;

        public CustomerController(ILogger<CustomerController> logger, IAcmeCorpBizService acmeCorpBizService)
        {
            _logger = logger;
            _acmeCorpBizService = acmeCorpBizService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            int customerId = await _acmeCorpBizService.CreateCustomerAsync(createCustomerRequest);

            return Created();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            Customer? customer = await _acmeCorpBizService.GetCustomerAsync(new GetEntityByIdRequest { Id = id });

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
