namespace ContactManager.DTOs
{
    public class ContactResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Email { get; set; }

        public string? City { get; set; }
    }
}
