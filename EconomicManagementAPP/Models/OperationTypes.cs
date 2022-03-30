using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicManagementAPP.Models
{
    public class OperationTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        [Remote(action: "VerificaryOperationType", controller: "OperationTypes")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        public string Description { get; set; }

    }
}
