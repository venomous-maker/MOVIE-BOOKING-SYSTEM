using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class DiscountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}