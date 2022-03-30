using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicManagementAPP.Models
{
    [Table("AccountTypes", Schema = "dbo")]
    public class AccountTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        [Remote(action: "VerificaryAccountType", controller: "AccountTypes")]//Activamos la validacion se dispara peticion http hacia el back 
        public string Name { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
    }
}
