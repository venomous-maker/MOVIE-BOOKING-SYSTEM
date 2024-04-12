﻿using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class SnackVM
{
    public int Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "Snack Name is required")]
    public string Name { get; set; }
    [Display(Name = "Snack Photo")]
    [Required(ErrorMessage = "Snack Photo is required")]
    public string SnackPhoto { get; set; }
    [Display(Name = "Review")]
    [Required(ErrorMessage = "Snack Review is required")]
    public string Review { get; set; }
    [Display(Name = "Price")]
    [Required(ErrorMessage = "Snack Price is required")]
    public int Price { get; set; }
}