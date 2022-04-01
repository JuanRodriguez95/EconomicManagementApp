using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicManagementAPP.Models
{
    [Table("Users",Schema="dbo")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        //[FirstCapitalLetter]

        [Remote(action: "VerificaryUsers", controller: "Users")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        //[FirstCapitalLetter]

        public string StandarEmail { get; set; }
        [Required(ErrorMessage = "{0} is required")]

        public string Password { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
    }
}
