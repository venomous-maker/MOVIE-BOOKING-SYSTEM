using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class ContactController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}