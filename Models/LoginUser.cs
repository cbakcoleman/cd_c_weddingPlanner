using System.ComponentModel.DataAnnotations;

namespace cd_c_weddingPlanner.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "You must enter an email.")]
        [Display(Name = "Email: ")]
        public string LoginEmail {get;set;}

        [Required(ErrorMessage = "You must enter a password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string LoginPassword {get;set;}
    }
}