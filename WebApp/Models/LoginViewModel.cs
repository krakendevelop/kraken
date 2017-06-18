using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
  public class LoginViewModel
  {
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }
}