using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Order : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        [NotMapped]
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
