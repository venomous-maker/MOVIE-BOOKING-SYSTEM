using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class SnackShoppingCartItem
{
    [Key]
    public int Id { get; set; }

   public string Name { get; set; }
    public int Amount { get; set; }
    public Snack Snack { get; set; }

    public string ShoppingCartId { get; set; }
        
    public int NumberTaken { get; set; }
}