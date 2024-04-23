using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrderDetail : IEntity<OrderDetailKey>
    {
        [Key]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;

        [NotMapped]
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
