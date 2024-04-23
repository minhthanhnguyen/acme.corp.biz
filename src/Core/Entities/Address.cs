using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Address : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AddressType { get; set; }  // Primary, Mailing, Shipping, Business

        [Required]
        public string Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }
    }
}
