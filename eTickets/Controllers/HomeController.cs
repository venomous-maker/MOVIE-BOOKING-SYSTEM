using System.Threading.Tasks;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class HomeController : Controller
{
    private readonly IMoviesService _service;

    public HomeController(IMoviesService service)
    {
        _service = service;
    }

    
    // GET
    public async Task<IActionResult> Index()
    {
        @ViewData["active"] = true;
        var allMovies = await _service.GetAllAsync(n => n.MovieCinemas);
        return View(allMovies);
    }
}