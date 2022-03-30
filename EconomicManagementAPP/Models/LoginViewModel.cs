using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="{0}")]
        [EmailAddress(ErrorMessage ="Invalid Format Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
