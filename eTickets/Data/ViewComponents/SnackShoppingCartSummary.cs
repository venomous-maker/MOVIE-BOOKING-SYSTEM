using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;


namespace eTickets.Data.ViewComponents;

public class SnackShoppingCartSummary:ViewComponent
{
    private readonly SnackShoppingCart _snackShoppingCart;
    public SnackShoppingCartSummary(SnackShoppingCart shoppingCart)
    {
        _snackShoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _snackShoppingCart.GetShoppingCartItems();

        return View(items.Count);
    }
}