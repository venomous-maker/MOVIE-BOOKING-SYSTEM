using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Movie_Cinema
{
    [Key]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; }
}