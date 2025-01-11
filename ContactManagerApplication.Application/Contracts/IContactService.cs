using ContactManagerApplication.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace ContactManagerApplication.Application.Contracts;

public interface IContactService
{
    Task<IEnumerable<ContactDto>> GetAllContactsAsync();
    Task AddContactsFromCsvAsync(IFormFile file);
    Task UpdateContactAsync(int id, ContactDto contact);
    Task DeleteContactAsync(int id);
}