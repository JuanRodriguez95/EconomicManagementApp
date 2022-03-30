using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Models
{
    public class AccountCreationViewModel : Accounts
    {
        public IEnumerable<SelectListItem> AccountTypes { get; set; }

    }
}
