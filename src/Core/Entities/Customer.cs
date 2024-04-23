using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Customer : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Address> Addresses { get; } = new List<Address>();

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
