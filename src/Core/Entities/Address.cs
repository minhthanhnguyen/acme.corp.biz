using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Address : IEntity<int>
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("addressType")]
        public string AddressType { get; set; }  // Primary, Mailing, Shipping, Business

        [Required]
        [JsonPropertyName("address1")]
        public string Address1 { get; set; }

        [JsonPropertyName("address2")]
        public string? Address2 { get; set; }

        [Required]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [Required]
        [JsonPropertyName("state")]
        public string State { get; set; }

        [Required]
        [JsonPropertyName("zipCode")]
        public string ZipCode { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }
    }
}
