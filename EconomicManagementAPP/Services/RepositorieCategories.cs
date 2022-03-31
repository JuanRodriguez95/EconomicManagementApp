using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
