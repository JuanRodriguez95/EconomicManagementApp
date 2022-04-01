using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieTransactions : GenericRepositorie<Transactions>
    {
        private readonly EconomicContext _context;
        private readonly RepositorieAccounts _repositorieAccounts;

        public RepositorieTransactions(EconomicContext context, RepositorieAccounts repositorieAccounts) : base(context)
        {
            _context = context;
            _repositorieAccounts = repositorieAccounts;
        }

        public async Task<IEnumerable<Transactions>> getTransactionByUserId(int userId)
        {
            var transaction = await _context.Transactions.Where(transactionDb => transactionDb.UserId == userId).ToListAsync();
            return transaction;
        }
    }
}
