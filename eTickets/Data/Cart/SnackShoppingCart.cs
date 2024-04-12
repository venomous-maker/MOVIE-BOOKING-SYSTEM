using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eTickets.Data.Cart
{
    
    public class SnackShoppingCart
    {
         public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<SnackShoppingCartItem> ShoppingCartItems { get; set; }

        public SnackShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static SnackShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new SnackShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Snack snack, int number)
        {
            var snackShoppingCart = _context.SnackShoppingCartItems.FirstOrDefault(n => n.Snack.Id == snack.Id && n.ShoppingCartId == ShoppingCartId);
            
            if(snackShoppingCart == null)
            {
                snackShoppingCart = new SnackShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Snack = snack,
                    Amount = 1,
                    NumberTaken = number
                };

                _context.SnackShoppingCartItems.Add(snackShoppingCart);
            } else
            {
                snackShoppingCart.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Snack snack)
        {
            var snackShoppingCart = _context.SnackShoppingCartItems.FirstOrDefault(n => n.Snack.Id == snack.Id && n.ShoppingCartId == ShoppingCartId);

            if (snackShoppingCart != null)
            {
                if(snackShoppingCart.Amount > 1)
                {
                    snackShoppingCart.Amount--;
                    
                } else
                {
                    _context.SnackShoppingCartItems.Remove(snackShoppingCart);
                }
            }
            _context.SaveChanges();
        }

        public List<SnackShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.SnackShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Snack).ToList());
        }

        public double GetShoppingCartTotal() =>  _context.SnackShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Snack.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.SnackShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.SnackShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    
    }
}
