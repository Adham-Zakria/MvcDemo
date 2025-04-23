using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Frist Name Can't be Empty")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!; 

        [Required(ErrorMessage = "Frist Name Can't be Empty")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
        public bool IsAgree { get; set; }

    }
}
