using System.Collections.Generic;
using System.Threading.Tasks;
using eTickets.Models;

namespace eTickets.Data.Services;

public interface ISnackOrderService
{
    Task StoreOrderAsync(List<SnackShoppingCartItem> items, string userId, string userEmailAddress);
    Task<List<SnackOrder>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
}