using EconomicManagementAPP.Models;
using EconomicManagementAPP.Data;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieOperationTypes : GenericRepositorie<OperationTypes>
    {
        private readonly EconomicContext _context;

        public RepositorieOperationTypes(EconomicContext context) : base(context)
        {
            _context = context;
        }

      
    }


}
