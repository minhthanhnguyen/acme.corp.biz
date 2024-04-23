using Core.CQRS;
using Core.Entities;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi;
using Xunit;

namespace Tests.IntegrationTests
{
    public class ProductControllerIntegrationTests : IClassFixture<WebApiTestFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerIntegrationTests(WebApiTestFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostAndGetActions()
        {
            var createProductRequest = new CreateProductRequest
            {
                Name = "Xbox",
                UnitPrice = 499m,
                UnitsInStock = 1000,
            };

            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync<CreateProductRequest>("/product/create", createProductRequest);

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            responseMessage = await _client.GetAsync("/product/1");

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            {
                Product? Product = await JsonSerializer.DeserializeAsync<Product>(stream);

                Xunit.Assert.NotNull(Product);
                Xunit.Assert.Equal(createProductRequest.Name, Product.Name);
                Xunit.Assert.Equal(createProductRequest.UnitPrice, Product.UnitPrice);
                Xunit.Assert.Equal(createProductRequest.UnitsInStock, Product.UnitsInStock);
            }
        }
    }
}
