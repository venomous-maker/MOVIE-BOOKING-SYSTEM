using System.Security.Claims;
using System.Threading.Tasks;
using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class SnackOrderController:Controller
    {
        private readonly ISnackService _snackService;
        private readonly SnackShoppingCart _snackShoppingCart;
        private readonly ISnackOrderService _snackOrderService;

        public SnackOrderController(ISnackService snackService, SnackShoppingCart snackShoppingCart, ISnackOrderService snackOrderService)
        {
            _snackService = snackService;
            _snackShoppingCart = snackShoppingCart;
            _snackOrderService = snackOrderService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _snackOrderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _snackShoppingCart.GetShoppingCartItems();
            _snackShoppingCart.ShoppingCartItems = items;

            var response = new SnackShoppingCartVM()
            {
                SnackShoppingCart = _snackShoppingCart,
                ShoppingCartTotal = _snackShoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id, int taken)
        {
            var item = await _snackService.GetSnackByIdAsync(id);

            if (item != null)
            {
                for (int i = 0; i < 1; i++)
                {
                    _snackShoppingCart.AddItemToCart(item, taken);
                }
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _snackService.GetSnackByIdAsync(id);

            if (item != null)
            {
                _snackShoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _snackShoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _snackOrderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _snackShoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}

