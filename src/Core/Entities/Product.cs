using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Product : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short UnitsInStock { get; set; }
    }
}
