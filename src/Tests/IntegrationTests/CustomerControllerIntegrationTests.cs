using Core.CQRS;
using Core.Entities;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi;
using Xunit;

namespace Tests.IntegrationTests
{
    public class CustomerControllerIntegrationTests : IClassFixture<WebApiTestFactory<Program>>
    {
        private readonly HttpClient _client;

        public CustomerControllerIntegrationTests(WebApiTestFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostAndGetActions()
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                FirstName = "Minh",
                LastName = "Nguyen",
                Email = "minh@test",
            };

            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync<CreateCustomerRequest>("/customer/create", createCustomerRequest);

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            responseMessage = await _client.GetAsync("/customer/1");

            // Assert
            responseMessage.EnsureSuccessStatusCode();

            using (var stream = await responseMessage.Content.ReadAsStreamAsync())
            {
                Customer? customer = await JsonSerializer.DeserializeAsync<Customer>(stream);

                Xunit.Assert.NotNull(customer);
                Xunit.Assert.Equal(createCustomerRequest.FirstName, customer.FirstName);
                Xunit.Assert.Equal(createCustomerRequest.LastName, customer.LastName);
                Xunit.Assert.Equal(createCustomerRequest.Email, customer.Email);
            }
        }
    }
}
