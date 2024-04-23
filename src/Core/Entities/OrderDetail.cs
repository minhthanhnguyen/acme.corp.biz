using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class OrderDetail : IEntity<OrderDetailKey>
    {
        [Key]
        [Column(Order = 0)]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("order")]
        public Order Order { get; set; } = null!;

        [JsonPropertyName("product")]
        public Product Product { get; set; } = null!;

        [NotMapped]
        [JsonPropertyName("id")]
        public OrderDetailKey Id 
        { 
            get 
            {
                return new OrderDetailKey { OrderId = this.OrderId, ProductId = this.ProductId };
            }
            set
            {
                this.OrderId = value.OrderId;
                this.ProductId = value.ProductId;
            }
        }
    }
}
