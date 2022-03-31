using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

namespace EconomicManagementAPP.Services
{
    

    public class RepositorieAccounts : GenericRepositorie<Accounts>
    {
        
        private readonly EconomicContext _context;

        public RepositorieAccounts(EconomicContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accounts>> GetAccounts(int userId)
        {
            var accounts = await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
            return accounts;
        }

        public async Task UpdateAccountTotal(Transactions transactions)
        {
            var account = await _context.Accounts.FindAsync(transactions.AccountId);
            if(transactions.OperationTypeId == 2)
            {
                account.Balance = account.Balance + transactions.Total;
            }
            else
            {
                account.Balance = account.Balance - transactions.Total;
            }
            await _context.SaveChangesAsync();
        } 
    }
}
