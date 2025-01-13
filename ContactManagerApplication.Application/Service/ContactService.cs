using System.Globalization;
using ContactManagerApplication.Application.Contracts;
using ContactManagerApplication.Application.DTOs;
using ContactManagerApplication.Domain.Entities;
using ContactManagerApplication.Infrastructure.Contracts;
using CsvHelper;
using Microsoft.AspNetCore.Http;

namespace ContactManagerApplication.Application.Service;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        return await _contactRepository.GetAllContactsAsync();
    }

    public async Task<Contact> GetContactByIdAsync(int id)
    {
        return await _contactRepository.GetContactByIdAsync(id);
    }

    public async Task AddContactAsync(Contact contact)
    {
        await _contactRepository.AddContactAsync(contact);
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        await _contactRepository.UpdateContactAsync(contact);
    }

    public async Task DeleteContactAsync(int id)
    {
        await _contactRepository.DeleteContactAsync(id);
    }
}