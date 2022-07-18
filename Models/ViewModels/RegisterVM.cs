using System.ComponentModel.DataAnnotations;

namespace AmiFlota.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }   
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage ="The password must be at least {2} characters long.",MinimumLength = 4)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } 
        [Required]
        public string Role { get; set; }    

    }
}
