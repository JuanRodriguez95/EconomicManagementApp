using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieAccountTypes : Repositorie<AccountTypes>
    {
        private readonly EconomicContext _context;


        public RepositorieAccountTypes(EconomicContext context) : base(context)
        {
            _context = context;
        }


        // El async va acompañado de Task
        public async Task<bool> Exist(string name)
        {
            var result = await _context.AccountTypes.Where(u => u.Name == name).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task Modify(AccountTypes accountTypes)
        {
            AccountTypes localAccountsTypes = new AccountTypes();
            localAccountsTypes = await _context.AccountTypes.FindAsync(accountTypes.Id);
            localAccountsTypes.Name = accountTypes.Name;
            await _context.SaveChangesAsync();
        }
    }
}
