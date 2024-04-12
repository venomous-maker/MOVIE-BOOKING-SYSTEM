using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }


        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var cinema = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Check if searchString is a valid zip code
                if (int.TryParse(searchString, out int zip))
                {
                    // Find cinemas within +-500 of the given zip code
                    var filteredResult = cinema.Where(n =>
                        n.ZipCode >= zip - 500 &&
                        n.ZipCode <= zip + 500
                    ).ToList();
            
                    return View("Index", filteredResult);
                }
                else
                {
                    // Search by city name if the searchString is not a valid zip code
                    var filteredResult = cinema.Where(n =>
                        n.City.Equals(searchString, StringComparison.InvariantCultureIgnoreCase)
                    ).ToList();

                    return View("Index", filteredResult);
                }
            }

            return View("Index", cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description, City, ZipCode, NumberOfSeats")]Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetCinemaByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description,ZipCode, NumberOfSeats")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
