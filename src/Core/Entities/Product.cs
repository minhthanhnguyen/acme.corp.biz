using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Product : IEntity<int>
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [Required]
        [JsonPropertyName("unitsInStock")]
        public short UnitsInStock { get; set; }
    }
}
