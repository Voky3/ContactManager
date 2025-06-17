using ContactManager.Models;

namespace ContactManager.Data

{
    public interface IContactRepository
    {
        Task CreateAsync(Contact contact);
        Task<IEnumerable<Contact>> GetAllAsync(string? filter, string? sortBy);
        Task<Contact?> GetByIdAsync(int id);        
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
    }
}
