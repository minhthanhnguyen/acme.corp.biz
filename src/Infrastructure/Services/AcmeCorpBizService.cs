using Core.CQRS;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Services
{
    public class AcmeCorpBizService : IAcmeCorpBizService
    {
        private readonly IGenericRepository<Customer, int> _customerRepository;

        public AcmeCorpBizService(IGenericRepository<Customer, int> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetCustomerAsync(GetCustomerRequest getCustomerRequest)
        {
            return await _customerRepository.GetSingleAsync(getCustomerRequest.Id);
        }

        public async Task CreateCustomerAsync(CreateCustomerRequest createCustomerRequest)
        {
            await _customerRepository.AddAsync(new Customer
            {
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,
            });
        }
    }
}
