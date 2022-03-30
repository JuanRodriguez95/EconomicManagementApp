using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;

namespace EconomicManagementAPP.Services
{
    
    public class RepositorieTransactions : Repositorie<Transactions>
    {
        private readonly EconomicContext _context;

        public RepositorieTransactions(EconomicContext context) : base(context)
        {
            _context = context;
        }

        public async Task Modify(Transactions transactions)
        {
            Transactions localTransactions = new Transactions();
            localTransactions = await _context.Transactions.FindAsync(transactions.Id);
            localTransactions.TransactionDate = transactions.TransactionDate;
            localTransactions.Total=transactions.Total;
            localTransactions.OperationTypeId = transactions.OperationTypeId;
            localTransactions.Description = transactions.Description;
            localTransactions.AccountId=transactions.AccountId;
            localTransactions.CategoryId = transactions.CategoryId;
            await _context.SaveChangesAsync();
        }
    }
}
