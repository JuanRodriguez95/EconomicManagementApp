using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicManagementAPP.Models
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        //public int UserId { get; set; }
        //[Required(ErrorMessage = "{0} is required")]
        public string TransactionDate { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Total { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int OperationTypeId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int CategoryId { get; set; }

    }
}
