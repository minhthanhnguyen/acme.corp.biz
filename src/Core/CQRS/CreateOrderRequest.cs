using System.Text.Json.Serialization;

namespace Core.CQRS
{
    public class CreateOrderRequest
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("orderLines")]
        public List<OrderLineRequest> OrderLines { get; set; } = new List<OrderLineRequest>();
    }

    public class OrderLineRequest
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}