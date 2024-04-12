using System.Threading.Tasks;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SnackController : Controller
    {
        private readonly ISnackService _service;

        public SnackController(ISnackService service)
        {
            _service = service;
        }

        // GET
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var snack = await _service.GetAllAsync();
            return View(snack);
        }

        public async Task<IActionResult> Details(int id)
        {
            var snack = await _service.GetSnackByIdAsync(id);
            return View(snack);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SnackVM snack)
        {
            await _service.AddNewSnackAsync(snack);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var snack = await _service.GetSnackByIdAsync(id);
            if (snack == null) return View("NotFound");

            var response = new SnackVM()
            {
                Id = snack.Id,
                Name = snack.Name,
                Price = snack.Price,
                SnackPhoto = snack.SnackPhoto
            };



            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SnackVM snack)
        {
            if (id != snack.Id) return View("NotFound");


            await _service.UpdateSnackAsync(snack);
            return RedirectToAction(nameof(Index));
        }

        
    }
}