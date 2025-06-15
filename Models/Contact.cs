using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        public string? City { get; set; }
    }
}
