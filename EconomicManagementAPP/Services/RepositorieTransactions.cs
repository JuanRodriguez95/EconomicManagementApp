using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieTransactions : GenericRepositorie<Transactions>
    {
        private readonly EconomicContext _context;

        public RepositorieTransactions(EconomicContext context) : base(context)
        {
            _context = context;
        }

    }
}
