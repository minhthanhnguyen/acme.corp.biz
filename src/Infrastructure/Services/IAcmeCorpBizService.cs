using Core.CQRS;
using Core.Entities;

namespace Infrastructure.Services
{
    public interface IAcmeCorpBizService
    {
        Task<Customer> GetCustomerAsync(GetCustomerRequest getCustomerRequest);

        Task CreateCustomerAsync(CreateCustomerRequest createCustomerRequest);
    }
}
