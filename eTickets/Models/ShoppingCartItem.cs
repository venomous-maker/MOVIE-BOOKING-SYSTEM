using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
        
        public int NumberOfSeats { get; set; }
        public DateTime DateTime { get; set; }
        public string Time { get; set; }
        public string Seats { get; set; }
        public int CinemaId { get; set; }
    }
}
