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
        
        
    }
}
