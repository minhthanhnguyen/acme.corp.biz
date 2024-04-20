using System.Text.Json.Serialization;

namespace Core.CQRS
{
    public class CreateCustomerRequest
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }
}
