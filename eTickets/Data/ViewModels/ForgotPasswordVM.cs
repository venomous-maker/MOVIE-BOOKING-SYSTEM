using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class ForgotPasswordVM
{
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; }
}