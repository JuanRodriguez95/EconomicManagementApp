using EconomicManagementAPP.Models;
using EconomicManagementAPP.Data;

namespace EconomicManagementAPP.Services
{
    public class RepositorieCategories : GenericRepositorie<Categories>
    {
        private readonly EconomicContext _context;

        public RepositorieCategories(EconomicContext context) : base(context)
        {
            _context = context;
        }
    }
}
