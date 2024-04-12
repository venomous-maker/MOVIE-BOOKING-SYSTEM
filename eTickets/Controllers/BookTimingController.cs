using System;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace eTickets.Controllers;

public class BookTimingController : Controller
{
    private readonly AppDbContext _context;

    public BookTimingController(AppDbContext context)
    {
        _context = context;
    }
    // GET
    [HttpGet("Index")]
    public async Task<IActionResult> Index(string time, int id, string price, int seats, int cinemaID, string day)
    {
        @ViewData["id"] = id;
        @ViewData["time"] = time;
        @ViewData["price"] = price;
        @ViewData["seats"] = seats;
        @ViewData["cinemaID"] = cinemaID;
        @ViewData["day"] = day;
        DateTime dateTime;
        if (DateTime.TryParseExact(day, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            // Parsing successful, dateTime now contains the parsed DateTime value
            Console.WriteLine("Parsed DateTime: " + dateTime);
        }
        else
        {
            // Parsing failed, handle the error accordingly
            Console.WriteLine("Failed to parse DateTime");
        }

        var orderItems =
              _context.OrderItems.Where(o => o.Time == time && o.DateTime == dateTime && o.MovieId == id && o.CinemaId == cinemaID).Select(o => o.Seat).ToList();
        string joinString = string.Join(",", orderItems);
        string[] seatsArray = joinString.Split(",");
        ViewBag.seatsArray = seatsArray;

        
        return View();
    }
}