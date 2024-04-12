using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models;

public class SnackOrder
{
    
    [Key]
    public int Id { get; set; }

    public string Email { get; set; }

    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    public List<SnackOrderItem> SnackOrderItems { get; set; }
}