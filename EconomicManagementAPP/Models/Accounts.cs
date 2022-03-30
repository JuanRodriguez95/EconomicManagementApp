using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicManagementAPP.Models
{
    [Table("Accounts", Schema = "dbo")]
    public class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        [Remote(action: "VerificaryAccount", controller: "Accounts")]//Activamos la validacion se dispara peticion http hacia el back 
        public string Name { get; set; }
        [ForeignKey("AccountTypes")]
        [Required(ErrorMessage = "{0} is required")]
        public int AccountTypeId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public Decimal Balance { get; set; }        
        public string Description { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }

        public AccountTypes AccountTypes { get; set; }
        public Users Users { get; set; }
    }
}
