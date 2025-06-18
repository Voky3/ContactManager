using ContactManager.Data;
using ContactManager.DTOs;
using ContactManager.Models;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository) => _contactRepository = contactRepository;

        public async Task CreateAsync(ContactRequest request)
        {
            var contact = MapToEntity(request);
            await _contactRepository.CreateAsync(contact);
        }        

        public async Task<IEnumerable<ContactResponse>> GetAllAsync(string? filter, string? sortBy)
        {
            var contacts = await _contactRepository.GetAllAsync(filter, sortBy);

            return contacts.Select(MapToResponse).ToList();
        }

        public async Task<ContactResponse?> GetByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
                return null;

            return MapToResponse(contact);
        }

        public async Task<bool> UpdateAsync(int id, ContactRequest request)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            UpdateEntity(existing, request);
            await _contactRepository.UpdateAsync(existing);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _contactRepository.DeleteAsync(id);
            return true;
        }

        private Contact MapToEntity(ContactRequest request) => new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            Email = request.Email,
            City = request.City
        };

        private ContactResponse MapToResponse(Contact contact) => new()
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Phone = contact.Phone,
            Email = contact.Email,
            City = contact.City
        };

        private void UpdateEntity(Contact entity, ContactRequest request)
        {
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Phone = request.Phone;
            entity.Email = request.Email;
            entity.City = request.City;
        }
    }
}
