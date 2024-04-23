using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Customer : IEntity<int>
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("addresses")]
        public ICollection<Address> Addresses { get; } = new List<Address>();

        [JsonPropertyName("orders")]
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
