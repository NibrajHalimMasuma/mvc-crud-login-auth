using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IDBeAcademy.ModelViews
{
    public class AccountUser
    {
        [Required, EmailAddress, StringLength(50, MinimumLength = 4)]
        public string UserEmail { get; set; }


        [Required, DataType(DataType.Password), StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }


        [Compare("Password"), DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

    }

    public class LoginUser
    {
        [Required, EmailAddress, StringLength(50, MinimumLength = 4)]
        public string UserEmail { get; set; }


        [Required, DataType(DataType.Password), StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }


        [DisplayName("Remember Me?")]
        public bool RememberMe { get; set; }

    }

}
