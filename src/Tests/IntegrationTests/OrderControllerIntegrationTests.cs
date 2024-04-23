using Core.CQRS;
using Core.Entities;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi;
using Xunit;

namespace Tests.IntegrationTests
{
    public class OrderControllerIntegrationTests : IClassFixture<WebApiTestFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrderControllerIntegrationTests(WebApiTestFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostAndGetActions()
        {
            HttpResponseMessage responseMessage = null;

            var createCustomerRequest = new CreateCustomerRequest
            {
                FirstName = "Minh",
                LastName = "Nguyen",
                Email = "minh@test",
            };

            // POST customer
            responseMessage = await _client.PostAsJsonAsync<CreateCustomerRequest>("/customer/create", createCustomerRequest);

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            // GET customer
            responseMessage = await _client.GetAsync("/customer/1");

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            Customer? customer = null;

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            {
                customer = await JsonSerializer.DeserializeAsync<Customer>(stream);

                Xunit.Assert.NotNull(customer);
                Xunit.Assert.Equal(createCustomerRequest.FirstName, customer.FirstName);
                Xunit.Assert.Equal(createCustomerRequest.LastName, customer.LastName);
                Xunit.Assert.Equal(createCustomerRequest.Email, customer.Email);
            }

            var createProductRequest = new CreateProductRequest
            {
                Name = "Xbox",
                UnitPrice = 499m,
                UnitsInStock = 1000,
            };

            // POST product
            responseMessage = await _client.PostAsJsonAsync<CreateProductRequest>("/product/create", createProductRequest);

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            // GET product
            responseMessage = await _client.GetAsync("/product/1");

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            Product? product = null;

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            {
                product = await JsonSerializer.DeserializeAsync<Product>(stream);

                Xunit.Assert.NotNull(product);
                Xunit.Assert.Equal(createProductRequest.Name, product.Name);
                Xunit.Assert.Equal(createProductRequest.UnitPrice, product.UnitPrice);
                Xunit.Assert.Equal(createProductRequest.UnitsInStock, product.UnitsInStock);
            }

            var createOrderRequest = new CreateOrderRequest
            {
                CustomerId = customer.Id,
                OrderLines =  new List<OrderLineRequest>
                {
                    new OrderLineRequest
                    {
                        ProductId = product.Id,
                        Quantity = 1,
                    }
                }
            };

            // POST order
            responseMessage = await _client.PostAsJsonAsync<CreateOrderRequest>("/order/create", createOrderRequest);

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            // GET order
            responseMessage = await _client.GetAsync("/order/1");

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            Order? order = null;

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            {
                order = await JsonSerializer.DeserializeAsync<Order>(stream);

                Xunit.Assert.NotNull(order);

                Xunit.Assert.Equal(createOrderRequest.CustomerId, order.CustomerId);
                Xunit.Assert.Equal(createOrderRequest.OrderLines[0].ProductId, order.OrderDetails[0].ProductId);
                Xunit.Assert.Equal(createOrderRequest.OrderLines[0].Quantity, order.OrderDetails[0].Quantity);
            }
        }
    }
}
