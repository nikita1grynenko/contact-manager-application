using ContactManagerApplication.Application.DTOs;
using ContactManagerApplication.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ContactManagerApplication.Application.Contracts;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact> GetContactByIdAsync(int id);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
}