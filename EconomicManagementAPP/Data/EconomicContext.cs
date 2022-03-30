using EconomicManagementAPP.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace EconomicManagementAPP.Data
{
    public class EconomicContext : DbContext
    {
        
        public EconomicContext(DbContextOptions<EconomicContext>options):base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AccountTypes> AccountTypes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<OperationTypes> OperationTypes { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

    }
}
