using System.Globalization;
using ContactManagerApplication.Application.Contracts;
using ContactManagerApplication.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApplication.Web.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async Task<IActionResult> Index()
    {
        var contacts = await _contactService.GetAllContactsAsync();
        return View(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> UploadCsvFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null, 
            MissingFieldFound = null 
        };

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, config))
        {
            try
            {
                var contacts = csv.GetRecords<Contact>().ToList();

                foreach (var contact in contacts)
                {
                    await _contactService.AddContactAsync(contact);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing CSV file: {ex.Message}");
            }
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateContact([FromBody] Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _contactService.UpdateContactAsync(contact);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteContact(int id)
    {
        try{
            await _contactService.DeleteContactAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}