using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

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
