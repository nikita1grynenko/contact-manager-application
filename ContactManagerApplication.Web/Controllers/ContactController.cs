using ContactManagerApplication.Application.Contracts;
using ContactManagerApplication.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApplication.Web.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var contacts = await _service.GetAllContactsAsync();
        return View(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        try
        {
            await _service.AddContactsFromCsvAsync(file);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ContactDto contact)
    {
        await _service.UpdateContactAsync(id, contact);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteContactAsync(id);
        return Ok();
    }
}