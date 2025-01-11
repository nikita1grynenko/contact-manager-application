using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApplication.Web.Controllers;

public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}