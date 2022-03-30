using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.Data;
using Microsoft.EntityFrameworkCore;

namespace EconomicManagementAPP.Services
{
    public class RepositorieCategories : Repositorie<Categories>
    {
        private readonly EconomicContext _context;

        public RepositorieCategories(EconomicContext context) : base(context)
        {
            _context = context;
        }


        // El async va acompañado de Task
        public async Task<bool> Exist(string name)
        {
            var result = await _context.Categories.Where(u => u.Name == name).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task Modify(Categories categories)
        {
            Categories localCategories = new Categories();
            localCategories = await _context.Categories.FindAsync(categories.Id);
            localCategories.Name = categories.Name;
            await _context.SaveChangesAsync();
        }
    }
}
