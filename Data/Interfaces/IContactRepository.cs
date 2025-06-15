using ContactManager.Models;

namespace ContactManager.Data.Interfaces

{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync(string? filter, string? sortBy);
        Task<Contact?> GetByIdAsync(int id);
        Task CreateAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
    }
}
