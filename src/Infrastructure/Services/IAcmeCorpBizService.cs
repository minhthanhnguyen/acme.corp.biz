using Core.CQRS;
using Core.Entities;

namespace Infrastructure.Services
{
    public interface IAcmeCorpBizService
    {
        Task<Customer?> GetCustomerAsync(GetEntityByIdRequest getCustomerRequest);

        Task<int> CreateCustomerAsync(CreateCustomerRequest createCustomerRequest);

        Task<Product?> GetProductAsync(GetEntityByIdRequest getProductRequest);

        Task<int> CreateProductAsync(CreateProductRequest createProductRequest);

        Task<Order?> GetOrderAsync(GetEntityByIdRequest getOrderRequest);

        Task<int> CreateOrderAsync(CreateOrderRequest createOrderRequest);
    }
}
