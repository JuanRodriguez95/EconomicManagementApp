using EconomicManagementAPP.Models;
using EconomicManagementAPP.Data;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieAccountTypes : GenericRepositorie<AccountTypes>
    {
        private readonly EconomicContext _context;


        public RepositorieAccountTypes(EconomicContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
