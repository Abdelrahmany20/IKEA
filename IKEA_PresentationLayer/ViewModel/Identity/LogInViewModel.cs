using System.ComponentModel.DataAnnotations;

namespace IKEA_PresentationLayer.ViewModel.Identity
{
    public class LogInViewModel
    {

        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]

        public string Password { get; set; } = null!;

        [Display(Name = "Remember Me")]

        public bool RememberMe { get; set; }

    }
}
