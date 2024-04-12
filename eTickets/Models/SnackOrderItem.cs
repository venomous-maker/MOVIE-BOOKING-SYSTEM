using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models;

public class SnackOrderItem
{
    public int Id { get; set; }

    public int Amount { get; set; }
    public double Price { get; set; }
    public int SnackID { get; set; }
    public Snack Snack { get; set; }
    public int SnackOrderId { get; set; }
    [ForeignKey("SnackOrderId")]
    public SnackOrder SnackOrder { get; set; }
    
}