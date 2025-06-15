using ContactManager.Data.Interfaces;
using ContactManager.Models;

namespace ContactManager.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public Task CreateAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAllAsync(string? filter, string? sortBy)
        {
            throw new NotImplementedException();
        }

        public Task<Contact?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
