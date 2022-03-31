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


        // El async va acompañado de Task
        /*
        public async Task<bool> Exist(string name)
        {
            var result = await _context.Accounts.Where(u => u.Name == name).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        

        public async Task Modify(Accounts accounts)
        {
            Accounts localAccounts = new Accounts();
            localAccounts = await _context.Accounts.FindAsync(accounts.Id);
            localAccounts.Name = accounts.Name;
            localAccounts.AccountTypeId=accounts.AccountTypeId;
            localAccounts.Balance = accounts.Balance;
            localAccounts.Description = accounts.Description;
            localAccounts.UserId = accounts.UserId;
            await _context.SaveChangesAsync();
        }
        */
    }
}
