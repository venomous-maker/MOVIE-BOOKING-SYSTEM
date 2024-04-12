using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class AboutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}