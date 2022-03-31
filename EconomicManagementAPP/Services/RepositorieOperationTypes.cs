using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

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
