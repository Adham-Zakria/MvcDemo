using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.RolesViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
