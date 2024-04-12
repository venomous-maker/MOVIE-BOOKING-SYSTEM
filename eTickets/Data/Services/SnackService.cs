using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class SnackService :  EntityBaseRepository<Snack>, ISnackService
{
    private readonly AppDbContext _context;

    public SnackService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddNewSnackAsync(SnackVM data)
    {
        var newSnack = new Snack()
        {
            Name = data.Name,
            Price = data.Price,
            Review = data.Review,
            SnackPhoto = data.SnackPhoto
        };
        await _context.Snacks.AddAsync(newSnack);
        await _context.SaveChangesAsync();

    }

    public async Task<Snack> GetSnackByIdAsync(int id)
    {
        return await _context.Snacks.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task UpdateSnackAsync(SnackVM data)
    {
        var snack = await _context.Snacks.FirstOrDefaultAsync(n => n.Id == data.Id);
        if (snack != null)
        {
            snack.Name = data.Name;
            snack.SnackPhoto = data.SnackPhoto;
            snack.Price = data.Price;
            snack.Review = data.Review;
            await _context.SaveChangesAsync();
        }
    }
}