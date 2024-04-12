using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class ResetPasswordVM
{
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; }
    
    [Display(Name = "New Password")]
    [Required(ErrorMessage = "New Password is required")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    
    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "Confirm is required")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}