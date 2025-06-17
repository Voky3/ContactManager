using ContactManager.DTOs;

namespace ContactManager.Services
{
    public interface IContactService
    {
        Task CreateAsync(ContactRequest request);
        Task<IEnumerable<ContactResponse>> GetAllAsync(string? filter, string? sortBy);
        Task<ContactResponse?> GetByIdAsync(int id);        
        Task<bool> UpdateAsync(int id, ContactRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
