using System.Globalization;
using ContactManagerApplication.Application.DTOs;
using ContactManagerApplication.Domain.Entities;
using ContactManagerApplication.Infrastructure.Contracts;
using CsvHelper;
using Microsoft.AspNetCore.Http;

namespace ContactManagerApplication.Application.Service;

public class ContactService
{
    private readonly IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
    {
        var contacts = await _repository.GetAllAsync();
        return contacts.Select(c => new ContactDto
        {
            Name = c.Name,
            DateOfBirth = c.DateOfBirth,
            Married = c.Married,
            Phone = c.Phone,
            Salary = c.Salary
        });
    }

    public async Task AddContactsFromCsvAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file.");

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<ContactDto>().ToList();
            var entities = records.Select(r => new Contact
            {
                Name = r.Name,
                DateOfBirth = r.DateOfBirth,
                Married = r.Married,
                Phone = r.Phone,
                Salary = r.Salary
            });

            foreach (var entity in entities)
            {
                await _repository.AddAsync(entity);
            }
        }
    }

    public async Task UpdateContactAsync(int id, ContactDto contact)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new KeyNotFoundException("Contact not found.");

        entity.Name = contact.Name;
        entity.DateOfBirth = contact.DateOfBirth;
        entity.Married = contact.Married;
        entity.Phone = contact.Phone;
        entity.Salary = contact.Salary;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteContactAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}