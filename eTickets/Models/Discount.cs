using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Discount
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Discount Promo Code")]
    public string DiscountCode { get; set; }
    
    public string Status { get; set; }
}