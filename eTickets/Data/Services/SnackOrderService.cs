using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class SnackOrderService : ISnackOrderService
{
    private readonly AppDbContext _context;
    public SnackOrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SnackOrder>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
    {
        var orders = await _context.SnackOrders.Include(n => n.SnackOrderItems).ThenInclude(n => n.Snack).Include(n => n.User).ToListAsync();

        if(userRole != "Admin")
        {
            orders = orders.Where(n => n.UserId == userId).ToList();
        }

        return orders;
    }

    public async Task StoreOrderAsync(List<SnackShoppingCartItem> items, string userId, string userEmailAddress)
    {
        var order = new SnackOrder()
        {
            UserId = userId,
            Email = userEmailAddress
        };
        await _context.SnackOrders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderItem = new SnackOrderItem()
            {
                Amount = item.Amount,
                SnackID = item.Snack.Id,
                SnackOrderId = order.Id,
                Price = item.Snack.Price
            };
            await _context.SnackOrderItems.AddAsync(orderItem);
        }
        await _context.SaveChangesAsync();
    }
}