using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Order : IEntity<int>
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [NotMapped]
        [JsonPropertyName("orderDetails")]
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
