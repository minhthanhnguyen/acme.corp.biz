using System.Text.Json.Serialization;

namespace Core.CQRS
{
    public class CreateProductRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("unitsInStock")]
        public short UnitsInStock { get; set; }
    }
}
