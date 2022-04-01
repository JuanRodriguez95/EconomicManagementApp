using EconomicManagementAPP.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EconomicManagementAPP.Models
{
    [Table("Transactions", Schema = "dbo")]
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name ="Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "{0} is required")]
        public Decimal Total { get; set; }
        [Required(ErrorMessage = "{0} is required")]

        [ForeignKey("OperationTypes")]
        [Display(Name ="Operation Type")]
        public int OperationTypeId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [FirstCapitalLetter]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [ForeignKey("Accounts")]
        [Display(Name = "Account")]
        public int AccountId { get; set; }

        [ForeignKey("Categories")]
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Users")]
        [Display(Name = "User")]
        public int UserId { get; set; }

        public Categories Categories { get; set; }
        public Accounts Accounts { get; set; }
        public OperationTypes OperationTypes { get; set; }
        public Users Users { get; set; }

    }
}
