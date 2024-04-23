using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.CQRS
{
    public class CreateCustomerRequest
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("addresses")]
        public List<AddressRequest> Addresses { get; set; } = new List<AddressRequest>();
    }

    public class AddressRequest
    {
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
    }
}
